using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAward.PhotoManagement.Tests.Mocks;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoActors.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;
using PhotoAward.ThumbnailService.Interfaces;
using PhotoAward.PhotoDb.Interfaces;

namespace PhotoAward.PhotoManagement.Tests
{
    [TestClass]
    public class PhotoMamangementTests
    {
        [TestMethod]
        public async Task AddPhotoAndGetPhoto()
        {
            //Arrange
            var email = "info@bridging-it.de";
            var clientMock = new Mock<IMemberManagement>();
            clientMock.Setup(r => r.GetMember(It.IsAny<string>())).Returns(
                Task.FromResult(new MemberDto() {Email = email}));
            var clientFactoryMock = new Mock<IMemberManagementClientFactory>();
            clientFactoryMock.Setup(m => m.CreateMemberManagementClient()).Returns(clientMock.Object);
            var reliableStateManager = new MockReliableStateManager();
            var stateMngrMock = new Mock<IPhotoManagementStates>();
            stateMngrMock.Setup(s => s.GetPhotoActorId(It.IsAny<ITransaction>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ConditionalValue<ActorId> (true,ActorId.CreateRandom())));
            stateMngrMock.Setup(s => s.CreateTransaction()).Returns(new Mock<ITransaction>().Object);
            var thumbnailServiceMock = new Mock<IThumbnailService>();
            var thumbnailClientFactoryMock = new Mock<IThumbnailClientFactory>();
            thumbnailClientFactoryMock.Setup(cm => cm.CreateThumbnailClient()).Returns(thumbnailServiceMock.Object);
            var photoActorClientFactoryMock = new Mock<IPhotoActorClientFactory>();
            var photoActorClientMock = new Mock<IPhotoActor>();
            var photoId = Guid.NewGuid();
            photoActorClientMock.Setup(a => a.SetPhotoAsync(It.IsAny<PhotoInfo>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PhotoInfo()
                {
                    Id = photoId
                }));
            photoActorClientMock.Setup(a => a.GetPhotoAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PhotoInfo()
                {
                    Id = photoId
                }));
            photoActorClientFactoryMock.Setup(r => r.CreateActorClient(It.IsAny<ActorId>())).Returns(photoActorClientMock.Object);
            var photoDbServiceMock = new Mock<IPhotoDbService>();
            var photoDbFactoryMock = new Mock<IPhotoDbClientFactory>();
            photoDbFactoryMock.Setup(m => m.CreatePhotoDbClient()).Returns(photoDbServiceMock.Object);

            var pm = new PhotoManagement(CreateServiceContext(), stateMngrMock.Object, reliableStateManager, 
                clientFactoryMock.Object,
                photoActorClientFactoryMock.Object,
                thumbnailClientFactoryMock.Object, photoDbFactoryMock.Object);
            var uploadData = new PhotoUploadData()
            {
                Email = email,
                FileName = @"c:\temp\a1.jpg"
            };
            //Act
            var result = await pm.AddPhotoAsync(uploadData);
            var result2 = await pm.GetPhotoAsync(photoId);
            //Assert
            Assert.IsNotNull(result);
        }

        private StatefulServiceContext CreateServiceContext()
        {
            return new StatefulServiceContext(
                new NodeContext(String.Empty, new NodeId(0, 0), 0, String.Empty, String.Empty),
                new MockCodePackageActivationContext(),
                String.Empty,
                new Uri("fabric:/PhotoManagerMock"),
                null,
                Guid.NewGuid(),
                0);
        }
    }
}

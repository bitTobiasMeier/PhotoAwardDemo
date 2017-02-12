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
using PhotoAward.PhotoActor.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.PhotoManagement.Tests
{
    [TestClass]
    public class PhotoMamangementTests
    {
        [TestMethod]
        public async Task AddPhotoAndGetPhoto()
        {
            PhotoActor.PhotoActor actor;
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
            var thumbnailCreatorMock = new Mock<IThumbnailCreator>();
            var photoActorClientFactoryMock = new Mock<IPhotoActorClientFactory>();
            var photoActorClientMock = new Mock<IPhotoActor>();
            var photoId = Guid.NewGuid();
            photoActorClientMock.Setup(a => a.SetPhoto(It.IsAny<PhotoInfo>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PhotoInfo()
                {
                    Id = photoId
                }));
            photoActorClientMock.Setup(a => a.GetPhoto(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PhotoInfo()
                {
                    Id = photoId
                }));
            photoActorClientFactoryMock.Setup(r => r.CreateClient(It.IsAny<ActorId>())).Returns(photoActorClientMock.Object);

            var pm = new PhotoManagement(CreateServiceContext(), stateMngrMock.Object, reliableStateManager, 
                clientFactoryMock.Object,
                photoActorClientFactoryMock.Object,
                thumbnailCreatorMock.Object);
            var uploadData = new PhotoUploadData()
            {
                Email = email,
                FileName = @"c:\temp\a1.jpg"
            };
            //Act
            var result = await pm.AddPhoto(uploadData);
            var result2 = await pm.GetPhoto(photoId);
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

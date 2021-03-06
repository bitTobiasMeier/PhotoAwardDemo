﻿using System;
using System.Runtime.Serialization;
using Microsoft.ServiceFabric.Actors;

namespace PhotoAward.PhotoActors
{
    [DataContract]
    internal class PhotoComment
    {
        //ToDO: objekte immutable machen
        [DataMember]
        public Guid ? Id { get; set; }
        [DataMember]
        public Guid AuthorId { get; set; }
        [DataMember]
        public DateTime CommentDate { get; set; }
        [DataMember]
        public string Comment { get; set; }
    }
}
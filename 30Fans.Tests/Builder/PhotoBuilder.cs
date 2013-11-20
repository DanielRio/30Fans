using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace _30Fans.Tests.Builder {
    public class PhotoBuilder {
        private Photo photo;

        public PhotoBuilder() {
            photo = new Photo(new Product(), null,null, null);
        }

        public Photo Build() {
            return photo;
        }

        public PhotoBuilder WithText(string text) {
            photo.Text = text;
            return this;
        }

        public PhotoBuilder WithId(long id) {
            photo.Id = id;
            return this;
        }
    } // class
}

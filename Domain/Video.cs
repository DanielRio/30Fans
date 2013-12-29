using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class VideoGroup {
        private string videoCategory;
        private string plattaform;
        private string identifier;

        public VideoGroup(string plattaform, string videoCategory, string identifier) {
            this.plattaform = plattaform;
            this.videoCategory = videoCategory;
            this.identifier = identifier;
        }

        public string VideoCategory {
            get { return videoCategory; }
        }

        public string Plattaform {
            get { return plattaform; }
        }

        public string Identifier {
            get { return identifier; }
        }

        public string GetUrl() {
            if (string.IsNullOrEmpty(plattaform))
                throw new InvalidOperationException("Can´t create a complete url without a plataform");
            if (string.IsNullOrEmpty(identifier))
                throw new InvalidOperationException("Can´t create a complete url without an identifier");
            return string.Format("{0}/{1}", plattaform, identifier);
        }
    } //class
}

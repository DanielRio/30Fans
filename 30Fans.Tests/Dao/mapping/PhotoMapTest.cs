using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Mapping.ByCode;
using System.Xml.Serialization;
using Dao.Mapping;

namespace _30Fans.Tests.Dao.mapping {
    [TestClass]
    public class PhotoMapTest {
        [TestMethod]
        public void CanGenerateXml() {
            var model = new ModelMapper();
            model.AddMapping(typeof(PhotoMap));

            var hbmMapping = model.CompileMappingForAllExplicitlyAddedEntities();
            var xmlSerializer = new XmlSerializer(hbmMapping.GetType());

            xmlSerializer.Serialize(Console.Out, hbmMapping);
        }        
    }// class
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Mapping.ByCode;
using Dao.Mapping;
using System.Xml.Serialization;

namespace _30Fans.Tests.Dao.mapping {
    [TestClass]
    public class CategoryItemMapTest {
        [TestMethod]
        public void CanGenerateXml() {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<CategoryItemMap>();

            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            var xmlSerializer = new XmlSerializer(hbmMapping.GetType());

            xmlSerializer.Serialize(Console.Out, hbmMapping);
        }
    }// class
}

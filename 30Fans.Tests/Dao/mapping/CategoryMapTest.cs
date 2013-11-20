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
    public class CategoryMapTest {
        [TestMethod]
        public void CanGenerateXml() {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<CategoryMap>();

            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            var xmlSerialize = new XmlSerializer(hbmMapping.GetType());

            xmlSerialize.Serialize(Console.Out, hbmMapping);
        }
    } // class
}

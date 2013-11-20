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
    public class ProductMapTest {
        [TestMethod]
        public void CanGenerateMapping() {
            var mapper = new ModelMapper();
            mapper.AddMapping<ProductMap>();

            var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            XmlSerializer xml = new XmlSerializer(hbmMapping.GetType());

            xml.Serialize(Console.Out, hbmMapping);
        } // class
    }
}

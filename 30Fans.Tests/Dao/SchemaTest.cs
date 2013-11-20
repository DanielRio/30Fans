using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;
using Dao;

namespace _30Fans.Tests.Dao {
    [TestClass, DeploymentItem(@".\hibernate.cfg.xml")]
    public class SchemaTest : IntegrityTest{
        [TestMethod]
        public void CanGenerateSchema() {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(Console.WriteLine, true);
        }// class
    }
}

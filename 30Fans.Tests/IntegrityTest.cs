using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Dao;

namespace _30Fans.Tests {
    //Hack to copy System.Data.SQLite.dll for the output directory (needed when executing tests by command line)
    //http://stackoverflow.com/questions/2719304/sqlite-assembly-not-copied-to-output-folder-for-unit-testing

    [DeploymentItem(@".\hibernate.cfg.xml")]
    [DeploymentItem(@".\x86\SQLite.Interop.dll")]
    [DeploymentItem(@".\System.Data.SQLite.dll")]    
    [TestClass]
    public class IntegrityTest {
        public IntegrityTest() {
            //This line above force load System.Data.SQLite.dll to output folder. We can solve this problem adding DeploymentItem as well.
            //System.Data.SQLite.SQLiteLog.Enabled = true;
        }

        protected void DeleteDatabaseIfExists() {
            if (File.Exists("test.db"))
                File.Delete("test.db");
        }

        protected void StartDatabase() {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
        }
    }// class
}

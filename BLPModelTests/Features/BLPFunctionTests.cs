using BLPModel.Features;
using BLPModel.Features;
using BLPModel.Model;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLPModel.Features.Tests
{
    [TestClass()]
    public class BLPFunctionTests
    {
        List<SubjectModel> subjectModel;
        List<ObjectModel> objectModel;
        public void addTestData()
        {
            var blpFunction = new Features.BLPFunction();
            subjectModel = new List<SubjectModel>{
                blpFunction.addSubject("alice", SecurityLevelEnum.S, SecurityLevelEnum.U),
                blpFunction.addSubject("bob", SecurityLevelEnum.C, SecurityLevelEnum.C),
                blpFunction.addSubject("eve", SecurityLevelEnum.U, SecurityLevelEnum.U),
            };
            objectModel = new List<ObjectModel>{
                blpFunction.addObject("pub.txt", SecurityLevelEnum.U),
                blpFunction.addObject("emails.txt", SecurityLevelEnum.C),
                blpFunction.addObject("username.txt", SecurityLevelEnum.S),
                blpFunction.addObject("password.txt", SecurityLevelEnum.TS),
            };
        }
        [TestMethod()]
        public void case1()  //Alice reads a C file, e.g., run the code
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var testResult = blpFunction.read("alice","emails.txt");

            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void case2()  //Alice reads a TS file, e.g., run the code
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var testResult = blpFunction.read("alice", "password.txt");
            Assert.IsFalse(testResult);
        }

        [TestMethod()]
        public void case3()  //Eve reads a U file, e.g., run the code: 
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var testResult = blpFunction.read("eve", "pub.txt");
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void case4()  //Eve reads a C file, e.g., run the code:  
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var testResult = blpFunction.read("eve", "emails.txt");
            Assert.IsFalse(testResult);
        }

        [TestMethod()]
        public void case5()  //Bob reads a TS file, e.g., run the code: 
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var testResult = blpFunction.read("bob", "password.txt");
            Assert.IsFalse(testResult);
        }

        [TestMethod()]
        public void case6()  //Alice reads a C file and then writes to a U file, e.g., run the code: 
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "emails.txt");
            var writeResult = blpFunction.write("alice", "pub.txt");

            Assert.IsTrue(readResult);
            Assert.IsFalse(writeResult);
        }

        [TestMethod()]
        public void case7()  //Alice reads a C file and then write to a TS file, e.g., run the code: 
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "emails.txt");
            var writeResult = blpFunction.write("alice", "password.txt");

            Assert.IsTrue(readResult);
            Assert.IsFalse(writeResult);
        }

        [TestMethod()]
        public void case8()  //Alice reads a C file, writes a C file, then reads a S file and writes to a C file, e.g., run the code: 
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "emails.txt");
            var writeResult = blpFunction.write("alice", "emails.txt");
            var readOthResult = blpFunction.read("alice", "username.txt");
            var writeOthResult = blpFunction.write("alice", "emails.txt");

            Assert.IsTrue(readResult);
            Assert.IsTrue(writeResult);
            Assert.IsTrue(readOthResult);
            Assert.IsFalse(writeOthResult);
        }

        [TestMethod()]
        public void case9()  //Alice reads a C file, writes a S file, then reads a TS file and writes to a C file, e.g., run the code: 
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "emails.txt");
            var writeResult = blpFunction.write("alice", "username.txt");
            var readOthResult = blpFunction.read("alice", "password.txt");
            var writeOthResult = blpFunction.write("alice", "emails.txt");

            Assert.IsTrue(readResult);
            Assert.IsTrue(writeResult);
            Assert.IsFalse(readOthResult);
            Assert.IsFalse(writeOthResult);
        }

        [TestMethod()]
        public void case10()  //Alice reads a U file, writes to a C file, and Bob reads the C file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "pub.txt");
            var writeResult = blpFunction.write("alice", "emails.txt");
            var readOthResult = blpFunction.read("bob", "emails.txt");

            Assert.IsTrue(readResult);
            Assert.IsTrue(writeResult);
            Assert.IsTrue(readOthResult);
        }

        [TestMethod()]
        public void case11()  //Alice reads a U file, writes to a S file, and Bob reads the S file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "pub.txt");
            var writeResult = blpFunction.write("alice", "username.txt");
            var readOthResult = blpFunction.read("bob", "username.txt");
            
            Assert.IsTrue(readResult);
            Assert.IsTrue(writeResult);
            Assert.IsFalse(readOthResult);
        }

        [TestMethod()]
        public void case12()  //Alice reads a U file, writes to a TS file, and Bob reads the TS file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "pub.txt");
            var writeResult = blpFunction.write("alice", "password.txt");
            var readOthResult = blpFunction.read("bob", "password.txt");

            Assert.IsTrue(readResult);
            Assert.IsFalse(writeResult);
            Assert.IsFalse(readOthResult);
        }

        [TestMethod()]
        public void case13()  //Alice reads a U file, writes to a C file, and Eve reads the C file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "pub.txt");
            var writeResult = blpFunction.write("alice", "emails.txt");
            var readOthResult = blpFunction.read("eve", "emails.txt");

            Assert.IsTrue(readResult);
            Assert.IsTrue(writeResult);
            Assert.IsFalse(readOthResult);
        }

        [TestMethod()]
        public void case14()  //Alice reads a C file, writes to a U file, and Eve reads the U file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "emails.txt");
            var writeResult = blpFunction.write("alice", "pub.txt");
            var readOthResult = blpFunction.read("eve", "pub.txt");

            Assert.IsTrue(readResult);
            Assert.IsFalse(writeResult);
            Assert.IsTrue(readOthResult);
        }

        [TestMethod()]
        public void case15()  //Alice changes her current level to S, then reads a S file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var setResult = blpFunction.setLevel("alice", SecurityLevelEnum.S);
            var readResult = blpFunction.read("alice", "username.txt");

            Assert.IsTrue(readResult);
        }

        [TestMethod()]
        public void case16()  //Alice reads a C file, changes her current level to U, then writes to a U file, and Eve reads the U file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "emails.txt");

            var setResult = false;
            var errorMessage = "";
            try
            {
                setResult = blpFunction.setLevel("alice", SecurityLevelEnum.U);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            var writeResult = blpFunction.write("alice", "pub.txt");
            var readOthResult = blpFunction.read("eve", "pub.txt");

            Assert.IsTrue(readResult);
            Assert.IsNotNull(errorMessage);
            Assert.IsFalse(writeResult);
            Assert.IsTrue(readOthResult);
        }

        [TestMethod()]
        public void case17()  //Alice reads a S file, changes her current level to C, then writes to a C file, and Eve reads the C file, e.g., run the code:
        {
            addTestData();
            var blpFunction = new Features.BLPFunction(subjectModel, objectModel);
            var readResult = blpFunction.read("alice", "username.txt");
            var setResult = false;
            var errorMessage = "";
            try
            {
                setResult = blpFunction.setLevel("alice", SecurityLevelEnum.C);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            var writeResult = blpFunction.write("alice", "emails.txt");
            var readOthResult = blpFunction.read("eve", "emails.txt");

            Assert.IsTrue(readResult);
            Assert.IsNotNull(errorMessage);
            Assert.IsFalse(writeResult);
            Assert.IsFalse(readOthResult);
        }

    }
}



#include "StdAfx.h"
using namespace System::Collections::Generic;
using namespace Microsoft::VisualStudio::TestTools::UnitTesting::Web;
using namespace Microsoft::VisualStudio::TestTools::UnitTesting;
using namespace ContentManagerMVC::Models;
namespace TestProject1 {
    using namespace System;
    ref class ScheduleTest;
    
    
    /// <summary>
///This is a test class for ScheduleTest and is intended
///to contain all ScheduleTest Unit Tests
///</summary>
	[TestClass]
	public ref class ScheduleTest
	{

	private: Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^  testContextInstance;
			 /// <summary>
			 ///Gets or sets the test context which provides
			 ///information about and functionality for the current test run.
			 ///</summary>
	public: property Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^  TestContext
			{
				Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^  get()
				{
					return testContextInstance;
				}
				System::Void set(Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^  value)
				{
					testContextInstance = value;
				}
			}

#pragma region Additional test attributes
			// 
			//You can use the following additional attributes as you write your tests:
			//
			//Use ClassInitialize to run code before running the first test in the class
			//public: [ClassInitialize]
			//static System::Void MyClassInitialize(TestContext^  testContext)
			//{
			//}
			//
			//Use ClassCleanup to run code after all tests in a class have run
			//public: [ClassCleanup]
			//static System::Void MyClassCleanup()
			//{
			//}
			//
			//Use TestInitialize to run code before running each test
			//public: [TestInitialize]
			//System::Void MyTestInitialize()
			//{
			//}
			//
			//Use TestCleanup to run code after each test has run
			//public: [TestCleanup]
			//System::Void MyTestCleanup()
			//{
			//}
			//
#pragma endregion
			/// <summary>
			///A test for Schedule Constructor
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void ScheduleConstructorTest()
			{
				Schedule^  target = (gcnew Schedule());
				Assert::Inconclusive(L"TODO: Implement code to verify target");
			}
			/// <summary>
			///A test for Contents
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void ContentsTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				ICollection<ScheduledItem^ >^  expected = nullptr; // TODO: Initialize to an appropriate value
				ICollection<ScheduledItem^ >^  actual;
				target->Contents = expected;
				actual = target->Contents;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Continuous
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void ContinuousTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Continuous = expected;
				actual = target->Continuous;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for EndTime
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void EndTimeTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				DateTime expected = DateTime(); // TODO: Initialize to an appropriate value
				DateTime actual;
				target->EndTime = expected;
				actual = target->EndTime;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Fri
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void FriTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Fri = expected;
				actual = target->Fri;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for ID
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void IDTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				int expected = 0; // TODO: Initialize to an appropriate value
				int actual;
				target->ID = expected;
				actual = target->ID;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Mon
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void MonTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Mon = expected;
				actual = target->Mon;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for PlayOnePerRound
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void PlayOnePerRoundTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->PlayOnePerRound = expected;
				actual = target->PlayOnePerRound;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Sat
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void SatTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Sat = expected;
				actual = target->Sat;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for StartTime
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void StartTimeTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				DateTime expected = DateTime(); // TODO: Initialize to an appropriate value
				DateTime actual;
				target->StartTime = expected;
				actual = target->StartTime;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Sun
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void SunTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Sun = expected;
				actual = target->Sun;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Thr
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void ThrTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Thr = expected;
				actual = target->Thr;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Title
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void TitleTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				String^  expected = System::String::Empty; // TODO: Initialize to an appropriate value
				String^  actual;
				target->Title = expected;
				actual = target->Title;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Tue
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void TueTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Tue = expected;
				actual = target->Tue;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for Wed
			///</summary>
			// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
			// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
			// whether you are testing a page, web service, or a WCF service.
	public: [TestMethod]
			[HostType(L"ASP.NET")]
			[AspNetDevelopmentServerHost(L"C:\\Users\\csq\\documents\\visual studio 2010\\Projects\\ContentManagerMVC\\ContentManag" 
				L"erMVC", 
				L"/")]
			[UrlToTest(L"http://localhost:56551/")]
			void WedTest()
			{
				Schedule^  target = (gcnew Schedule()); // TODO: Initialize to an appropriate value
				bool expected = false; // TODO: Initialize to an appropriate value
				bool actual;
				target->Wed = expected;
				actual = target->Wed;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
	};
}
namespace TestProject1 {
    
}

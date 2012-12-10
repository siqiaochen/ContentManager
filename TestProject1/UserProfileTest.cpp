
#include "StdAfx.h"
using namespace Microsoft::VisualStudio::TestTools::UnitTesting::Web;
using namespace Microsoft::VisualStudio::TestTools::UnitTesting;
using namespace ContentManagerMVC::Models;
namespace TestProject1 {
    using namespace System;
    ref class UserProfileTest;
    
    
    /// <summary>
///This is a test class for UserProfileTest and is intended
///to contain all UserProfileTest Unit Tests
///</summary>
	[TestClass]
	public ref class UserProfileTest
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
			///A test for UserProfile Constructor
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
			void UserProfileConstructorTest()
			{
				UserProfile^  target = (gcnew UserProfile());
				Assert::Inconclusive(L"TODO: Implement code to verify target");
			}
			/// <summary>
			///A test for UserId
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
			void UserIdTest()
			{
				UserProfile^  target = (gcnew UserProfile()); // TODO: Initialize to an appropriate value
				int expected = 0; // TODO: Initialize to an appropriate value
				int actual;
				target->UserId = expected;
				actual = target->UserId;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
			/// <summary>
			///A test for UserName
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
			void UserNameTest()
			{
				UserProfile^  target = (gcnew UserProfile()); // TODO: Initialize to an appropriate value
				String^  expected = System::String::Empty; // TODO: Initialize to an appropriate value
				String^  actual;
				target->UserName = expected;
				actual = target->UserName;
				Assert::AreEqual(expected, actual);
				Assert::Inconclusive(L"Verify the correctness of this test method.");
			}
	};
}
namespace TestProject1 {
    
}

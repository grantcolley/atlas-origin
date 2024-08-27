using Origin.Core.Extensions;
using Origin.Core.Model;
using Origin.Test.Data;

namespace Origin.Tests
{
    [TestClass]    
    public class DocumentConfig_CollapseSubstituteGroups
    {
        public string CONTENT = "Jane Masters, this is to inform you of an upcoming event.";

        [TestMethod]
        public void Collapse_Group_Intermittent_Spaces_Pass()
        {
            // Arrange
            List<DocumentSubstitute> documentSubstitutes = [];
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_NAME, Value = "Global Banking Corp." });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_1, Value = "9 Cherry Tree Lane" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_2, Value = "Canary Wharf" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_ADDRESS_3, Value = "E14 5HQ" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_PHONE_NUMBER, Value = "+44 071 946-0241" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.COMPANY_EMAIL, Value = "gbc@email.com" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_NAME, Value = "Mrs Jane Masters" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_1, Value = "142 Middle Street", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_2, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_3, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_4, Value = "Brockham", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_5, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_6, Value = "Surrey", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_7, Value = "KT20 3AD", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_REFERENCE, Value = "MASTERS/ABC-123" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.SIGNEE, Value = "Mrs Peggy Olson" });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.SIGNEE_TITLE, Value = "Managing Director" });

            DocumentConfig documentArgs = new()
            {
                Substitutes = documentSubstitutes
            };

            // Act
            documentArgs.CollapseSubstituteGroups();

            // Assert
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_1).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_2).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_3).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_4).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_5).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_6).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_7).Value);
            Assert.AreEqual("142 Middle Street", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_1).Value);
            Assert.AreEqual("Brockham", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_2).Value);
            Assert.AreEqual("Surrey", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_3).Value);
            Assert.AreEqual("KT20 3AD", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_4).Value);
        }

        [TestMethod]
        public void Collapse_Group_First_And_Last_Substitute_Empty_Pass()
        {
            // Arrange
            List<DocumentSubstitute> documentSubstitutes = [];

            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_1, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_2, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_3, Value = "142 Middle Street", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_4, Value = "Brockham", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_5, Value = "Surrey", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_6, Value = "KT20 3AD", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_7, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });

            DocumentConfig documentArgs = new()
            {
                Substitutes = documentSubstitutes
            };

            // Act
            documentArgs.CollapseSubstituteGroups();

            // Assert
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_1).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_2).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_3).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_4).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_5).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_6).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_7).Value);
            Assert.AreEqual("142 Middle Street", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_1).Value);
            Assert.AreEqual("Brockham", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_2).Value);
            Assert.AreEqual("Surrey", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_3).Value);
            Assert.AreEqual("KT20 3AD", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_4).Value);
        }

        [TestMethod]
        public void Collapse_Group_Random_Order_With_Empty_Substitutes_Pass()
        {
            // Arrange
            List<DocumentSubstitute> documentSubstitutes = [];

            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_7, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_1, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_6, Value = "KT20 3AD", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_3, Value = null, Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_4, Value = "Brockham", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_2, Value = "142 Middle Street", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });
            documentSubstitutes.Add(new DocumentSubstitute { Key = Substitutes.CUSTOMER_ADDRESS_5, Value = "Surrey", Group = Substitutes.CUSTOMER_ADDRESS_GROUP });

            DocumentConfig documentArgs = new()
            {
                Substitutes = documentSubstitutes
            };

            // Act
            documentArgs.CollapseSubstituteGroups();

            // Assert
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_1).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_2).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_3).Value);
            Assert.IsNotNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_4).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_5).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_6).Value);
            Assert.IsNull(documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_7).Value);
            Assert.AreEqual("142 Middle Street", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_1).Value);
            Assert.AreEqual("Brockham", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_2).Value);
            Assert.AreEqual("Surrey", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_3).Value);
            Assert.AreEqual("KT20 3AD", documentArgs.Substitutes.First(s => s.Key == Substitutes.CUSTOMER_ADDRESS_4).Value);
        }
    }
}
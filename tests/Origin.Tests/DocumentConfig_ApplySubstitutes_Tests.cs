using Origin.Core.Extensions;
using Origin.Core.Models;

namespace Origin.Tests
{
    [TestClass]    
    public class DocumentConfig_ApplySubstitutes_Tests
    {
        public string CONTENT = "Jane Masters, this is to inform you of an upcoming event.";

        [TestMethod]
        public void Content_With_No_Substitute_Keys_Unchanged_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = { new DocumentSubstitute { Key = "[Date]", Value = DateTime.Today.ToLongDateString() } },
                Contents = { new DocumentContent { Content = "Jane Masters, this is to inform you of an upcoming event." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual(CONTENT, documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Substitute_Full_Content_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = { new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" } },
                Contents = { new DocumentContent { Content = "[NAME]" } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual(documentArgs.Substitutes.First().Value, documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Substitute_Content_Starting_With_Key_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = { new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" } },
                Contents = { new DocumentContent { Content = "[NAME], this is to inform you of an upcoming event." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual(CONTENT, documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Substitute_Content_Ends_With_Key_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = { new DocumentSubstitute { Key = "ACTIVITY", Value = "event." } },
                Contents = { new DocumentContent { Content = "Jane Masters, this is to inform you of an upcoming [ACTIVITY]" } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual(CONTENT, documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Substitute_Content_Multiple_Keys_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = 
                {
                    new DocumentSubstitute { Key = "ACTIVITY", Value = "event" },
                    new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" },
                    new DocumentSubstitute { Key = "NOTIFICATION", Value = "inform" }
                },
                Contents = { new DocumentContent { Content = "[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual(CONTENT, documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Content_Null_Returns_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes =
                {
                    new DocumentSubstitute { Key = "ACTIVITY", Value = "event" },
                    new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" },
                    new DocumentSubstitute { Key = "NOTIFICATION", Value = "inform" }
                },
                Contents = { new DocumentContent() }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.IsNull(documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Substitutes_Empty_Content_Unchanged_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes = [],
                Contents = { new DocumentContent { Content = "[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual("[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY].", documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void Substitutes_Null_Content_Unchanged_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Contents = { new DocumentContent { Content = "[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual("[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY].", documentArgs.Contents.First().Content);
        }

        [TestMethod]
        public void SubstituteStart_Null_SubstituteEnd_Null_Content_Unchanged_Pass()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                Substitutes =
                {
                    new DocumentSubstitute { Key = "ACTIVITY", Value = "event" },
                    new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" },
                    new DocumentSubstitute { Key = "NOTIFICATION", Value = "inform" }
                },
                Contents = { new DocumentContent { Content = "[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();

            // Assert
            Assert.AreEqual("[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY].", documentArgs.Contents.First().Content);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "substituteEnd ] missing corresponding substituteStart")]
        public void SubstituteStart_Null_SubstituteEnd_Not_Null_Expected_InvalidDataException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteEnd = ']',
                Contents = { new DocumentContent { Content = "[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "substituteStart [ missing corresponding substituteEnd")]
        public void SubstituteStart_Not_Null_SubstituteEnd_Null_Expected_InvalidDataException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                Contents = { new DocumentContent { Content = "[NAME], this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "substituteStart missing corresponding substituteEnd")]
        public void SubstituteStart_Missing_Corresponding_SubstituteEnd_1_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes =
                {
                    new DocumentSubstitute { Key = "ACTIVITY", Value = "event" },
                    new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" },
                    new DocumentSubstitute { Key = "NOTIFICATION", Value = "inform" }
                },
                Contents = { new DocumentContent { Content = "[NAME, this is to [NOTIFICATION] you of an upcoming [ACTIVITY]." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "substituteStart missing corresponding substituteEnd")]
        public void SubstituteStart_Missing_Corresponding_SubstituteEnd_2_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes =
                {
                    new DocumentSubstitute { Key = "ACTIVITY", Value = "event" },
                    new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" },
                    new DocumentSubstitute { Key = "NOTIFICATION", Value = "inform" }
                },
                Contents = { new DocumentContent { Content = "[NAME, this is to notify you of an upcoming event." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "substituteEnd missing corresponding substituteStart")]
        public void SubstituteEnd_Missing_Corresponding_SubstituteStart_Expected_ArgumentNullException()
        {
            // Arrange
            DocumentConfig documentArgs = new()
            {
                SubstituteStart = '[',
                SubstituteEnd = ']',
                Substitutes =
                {
                    new DocumentSubstitute { Key = "ACTIVITY", Value = "event" },
                    new DocumentSubstitute { Key = "NAME", Value = "Jane Masters" },
                    new DocumentSubstitute { Key = "NOTIFICATION", Value = "inform" }
                },
                Contents = { new DocumentContent { Content = "NAME], this is to notify you of an upcoming event." } }
            };

            // Act
            documentArgs.ApplySubstitutesToDocumentContent();
        }
    }
}
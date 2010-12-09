using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace TechTalk.SpecFlow.Generator.UnitTestProvider
{
    public class MbUnitTestGeneratorProvider : IUnitTestGeneratorProvider
    {
        private const string TESTFIXTURE_ATTR = "MbUnit.Framework.TestFixtureAttribute";
        private const string TEST_ATTR = "MbUnit.Framework.TestAttribute";
        private const string CATEGORY_ATTR = "MbUnit.Framework.CategoryAttribute";
        private const string TESTSETUP_ATTR = "MbUnit.Framework.SetUpAttribute";
        private const string TESTFIXTURESETUP_ATTR = "MbUnit.Framework.FixtureSetUpAttribute";
        private const string TESTFIXTURETEARDOWN_ATTR = "MbUnit.Framework.FixtureTearDownAttribute";
        private const string TESTTEARDOWN_ATTR = "MbUnit.Framework.TearDownAttribute";
        private const string IGNORE_ATTR = "MbUnit.Framework.IgnoreAttribute";
        private const string DESCRIPTION_ATTR = "MbUnit.Framework.DescriptionAttribute";

        public bool SupportsTestCases { get { return false; } }

        public void SetTestFixture(CodeTypeDeclaration typeDeclaration, string title, string description)
        {
            typeDeclaration.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(TESTFIXTURE_ATTR)));

            SetDescription(typeDeclaration.CustomAttributes, title);
        }

        private void SetDescription(CodeAttributeDeclarationCollection customAttributes, string description)
        {
            customAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(DESCRIPTION_ATTR),
                    new CodeAttributeArgument(
                        new CodePrimitiveExpression(description))));
        }

        private void SetCategories(CodeAttributeDeclarationCollection customAttributes, IEnumerable<string> categories)
        {
            foreach (var category in categories)
            {
                customAttributes.Add(
                    new CodeAttributeDeclaration(
                        new CodeTypeReference(CATEGORY_ATTR),
                        new CodeAttributeArgument(
                            new CodePrimitiveExpression(category))));
            }
        }

        public void SetTestFixtureCategories(CodeTypeDeclaration typeDeclaration, IEnumerable<string> categories)
        {
            SetCategories(typeDeclaration.CustomAttributes, categories);
        }

        public void SetTest(CodeMemberMethod memberMethod, string title)
        {
            memberMethod.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(TEST_ATTR)));

            SetDescription(memberMethod.CustomAttributes, title);
        }

        public void SetTestCase(CodeMemberMethod memberMethod, IEnumerable<string> arguments)
        {
            // xUnit does not support test cases
            throw new NotSupportedException();
        }

        public void SetTestCategories(CodeMemberMethod memberMethod, IEnumerable<string> categories)
        {
            SetCategories(memberMethod.CustomAttributes, categories);
        }

        public void SetTestSetup(CodeMemberMethod memberMethod)
        {
            memberMethod.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(TESTSETUP_ATTR)));
        }

        public void SetTestFixtureSetup(CodeMemberMethod memberMethod)
        {
            memberMethod.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(TESTFIXTURESETUP_ATTR)));
        }

        public void SetTestFixtureTearDown(CodeMemberMethod memberMethod)
        {
            memberMethod.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(TESTFIXTURETEARDOWN_ATTR)));
        }

        public void SetTestTearDown(CodeMemberMethod memberMethod)
        {
            memberMethod.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(TESTTEARDOWN_ATTR)));
        }

        public void SetIgnore(CodeTypeMember codeTypeMember)
        {
            codeTypeMember.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(IGNORE_ATTR)));
        }
    }
}
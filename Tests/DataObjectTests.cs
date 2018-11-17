using NI.TestUtilities;
using System;
using System.Windows;
using Xunit;

namespace SyncContext_Bug
{
    public class SyncContextTests : AutoTest, IAsyncTest
    {
        [Fact]
        public void DataObject_Fails()
        {
            this.RunTest(() =>
            {
                var data = new TestClipboardData("ABC");
                var dataObject = new DataObject();
                dataObject.SetData(data);
                Clipboard.SetDataObject(dataObject);

                IDataObject clipboardData = Clipboard.GetDataObject();
                Assert.NotNull(clipboardData);

                var testClipboardData = clipboardData.GetData(typeof(TestClipboardData)) as TestClipboardData;
                Assert.NotNull(testClipboardData);
            });
        }

        [Serializable]
        internal class TestClipboardData
        {
            public string Text { get; private set; }

            public TestClipboardData(string text)
            {
                Text = text;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SOTags.DataAccess.Components;
using SOTags.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOTags.ApplicationServices.Components.Connectors.StackOverflow;
using SOTags.ApplicationServices;
using Newtonsoft.Json.Linq;
using SOTags.ApplicationServices.Components;

namespace SOTags.Tests.UnitTests.SOTags.ApplicationServicesTests
{
    public class StackOverflowConnectorTests
    {
        [Fact]
        public async Task StackOverflowConnector_DownloadsRequiredTagsAndSaveToJsonFile()
        {
            // Arrange
            var connector = new StackOverflowConnector();

            // Act
            await connector.DownloadData();

            // Assert
            Assert.True(File.Exists(Config.stackOverflowJsonFile));
            string jsonContent = File.ReadAllText(Config.stackOverflowJsonFile);
            Assert.False(string.IsNullOrEmpty(jsonContent));
            Assert.True(IsContentCorrect(jsonContent));
        }

        private bool IsContentCorrect(string jsonContent)
        {
            try
            {
                var reader = new StackOverflowJsonReader();
                var resultCollection = reader.ReadFile();
                if (resultCollection.Count > 0)
                {
                    var sample = resultCollection[0];
                    if (sample.Name != null && sample.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

using MediatR;
using SOTags.ApplicationServices.API.Domain;
using SOTags.ApplicationServices.Components;
using SOTags.ApplicationServices.Components.Connectors.StackOverflow;
using SOTags.DataAccess.CQRS;
using SOTags.DataAccess.CQRS.Commands;
using SOTags.DataAccess.Entities;

namespace SOTags.ApplicationServices.API.Handlers
{
    public class UpdateDatabaseHandler : IRequestHandler<UpdateDatabaseRequest, UpdateDatabaseResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly IStackOverflowConnector connector;
        private readonly IStackOverflowJsonReader jsonReader;

        public UpdateDatabaseHandler(ICommandExecutor commandExecutor, IStackOverflowConnector connector, IStackOverflowJsonReader jsonReader)
        {
            this.commandExecutor = commandExecutor;
            this.connector = connector;
            this.jsonReader = jsonReader;
        }
        public async Task<UpdateDatabaseResponse> Handle(UpdateDatabaseRequest request, CancellationToken cancellationToken)
        {
            List<Tag> tagList = new List<Tag>();
            await connector.DownloadData();
            if (connector.Error == null)
            {
                tagList = jsonReader.ReadFile();
            }

            var command = new UpdateDatabaseCommand()
            {
                Parameter = tagList
            };
            var information = await commandExecutor.Execute(command);
            var response = new UpdateDatabaseResponse()
            {
                Data = information
            };
            return response;
        }
    }
}

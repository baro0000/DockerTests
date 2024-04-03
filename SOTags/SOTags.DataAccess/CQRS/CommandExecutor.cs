using SOTags.DataAccess.CQRS.Commands;

namespace SOTags.DataAccess.CQRS
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly DatabaseDbContext context;

        public CommandExecutor(DatabaseDbContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TParameter, TResult>(CommandBase<TParameter, TResult> command)
        {
            return command.Execute(this.context);
        }
    }
}

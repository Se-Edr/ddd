using Mediatator.Core.ComsQueries;

namespace Application.CQRS.TestingHandl
{
    public record TestComm():ICommand<string>;
    public class TestCommandHandler : IRequestHandler<TestComm, string>
    {
        public async Task<string> Handle(TestComm request)
        {
            string res = "Some Response";
            return res ;
        }
    }
}

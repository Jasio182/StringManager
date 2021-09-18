using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.DataAnalize;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class UpdateDatesForMyInstrumentHandler : IRequestHandler<UpdateDatesForMyInstrumentRequest, UpdateDatesForMyInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMediator mediator;
        private readonly ILogger<UpdateDatesForMyInstrumentHandler> logger;

        public UpdateDatesForMyInstrumentHandler(IQueryExecutor queryExecutor,
                                                 IMediator mediator,
                                                 ILogger<UpdateDatesForMyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<UpdateDatesForMyInstrumentResponse> Handle(UpdateDatesForMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            var query = new GetMyInstrumentForDateUpdate()
            {
                Id = request.MyInstrumentId
            };
            var myInstrumentFromDb = await queryExecutor.Execute(query);
            var dateCalculator = new DateCalculator(myInstrumentFromDb, myInstrumentFromDb.User);
            var currentStrings = myInstrumentFromDb.InstalledStrings.Select(InstalledString => InstalledString.String).ToArray();
            var stringDate = dateCalculator.NumberOfDaysForStrings(request.Date, currentStrings);
            var cleanDate = dateCalculator.NumberOfDaysForCleaning(request.Date);
            var newRequest = new ModifyMyInstrumentRequest()
            {
                NextStringChange = stringDate,
                NextDeepCleaning = cleanDate,
                LastStringChange = request.Date,
                LastDeepCleaning = request.Date //think through
            };
            var response = await mediator.Send(newRequest);
            return new UpdateDatesForMyInstrumentResponse()
            {
                Data = response.Data
            };
        }
    }
}

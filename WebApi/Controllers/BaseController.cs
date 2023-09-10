using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BaseController:ControllerBase
    {
        private IMediator? _mediator;
        //eğer daha önce mediator enjekte edilmiş ise onu(_mediator) döndür, değilse verdiğimi http yapısını döndür.
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        
    }
}

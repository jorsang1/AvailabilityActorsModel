using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using TestDispoActors.Actors;
using TestDispoActors.Actors.Messages;

namespace TestDispoActors.Controllers
{
    [Route("api/[controller]")]
    public class PropertyController : Controller
    {
        private readonly IActorRef _availabilityManager;

        public PropertyController(ActorSystem actorSystem, IActorRef availabilityManager)
        {
            _availabilityManager = availabilityManager;
        }

        [HttpGet("list/{regionId}/")]
        public async Task<object> List(int regionId)
        {
            var message = new GetPropertyListRequest(1, regionId, null, null);
            var asyncResult = _availabilityManager.Ask(message);
            return await asyncResult;
        }

        [HttpGet("create/{regionId}/{propertyId}")]
        public async Task<object> CreateAsync(int regionId, int propertyId)
        {
            var message = new RequestAddProperty(regionId, propertyId);
            return await _availabilityManager.Ask(message);
        }

        // DELETE api/properties/1/5
        [HttpDelete("{regionId}/{propertyId}")]
        public async Task<object> Delete(int regionId, int propertyId)
        {
            var message = new RequestDeleteProperty(regionId, propertyId);
            return await _availabilityManager.Ask(message);
        }


    }
}

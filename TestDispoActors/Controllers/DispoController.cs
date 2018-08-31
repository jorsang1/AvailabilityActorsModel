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
    public class DispoController : Controller
    {
        private readonly ActorSystem _actorSystem;
        private readonly IActorRef _availabilityManager;

        public DispoController(ActorSystem actorSystem, IActorRef availabilityManager)
        {
            _actorSystem = actorSystem;
            _availabilityManager = availabilityManager;
        }

        //http://localhost:53958/api/dispo/create/1/4
        [HttpGet("create/{regionId}/{propertyId}")]
        public async Task<object> CreateAsync(int regionId, int propertyId)
        {
            var message = new RequestAddProperty(regionId, propertyId);
            return await _availabilityManager.Ask(message);
        }

        //http://localhost:53958/api/dispo/properties/1
        [HttpGet("properties/{regionId}/")]
        public async Task<object> Properties(int regionId)
        {
            var message = new GetPropertyListRequest(1, regionId, null, null);
            var asyncResult = _availabilityManager.Ask(message);
            return await asyncResult;
        }





        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var actor = _actorSystem.ActorSelection(id.ToString());
            return actor.PathString;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}

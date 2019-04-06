using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheCodeCamp.Data;
using TheCodeCamp.Data.Models;

namespace TheCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var result = await _campRepository.GetAllCampsAsync(includeTalks);

                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{moniker}", Name = "GetCamp")]
        public async Task<ActionResult> Get(string moniker, bool includeTalks = false)
        {
            try
            {
                var result = await _campRepository.GetCampAsync(moniker, includeTalks);

                if (result == null)
                {
                    return NotFound();
                }

                var mappedResult = _mapper.Map<CampModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Route("SearchByEventDate/{eventDate:datetime}")]
        [HttpGet]
        public async Task<ActionResult> SearchByEventDate(DateTime eventDate, bool includeTalks = false)
        {
            try
            {
                var result = await _campRepository.GetAllCampsByEventDate(eventDate, includeTalks);

                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Route("")]
        public async Task<ActionResult> Post(CampModel model)
        {
            try
            {
                if (await _campRepository.GetCampAsync(model.Moniker) != null)
                {
                    ModelState.AddModelError("Moniker", "Moniker in use");
                }

                if (ModelState.IsValid)
                {
                    var mapped = _mapper.Map<Camp>(model);

                    _campRepository.AddCamp(mapped);

                    if (await _campRepository.SaveChangesAsync())
                    {
                        var mappedCamp = _mapper.Map<CampModel>(mapped);

                        return CreatedAtRoute("GetCamp", new { moniker = mappedCamp.Moniker }, mappedCamp);

                        //return Created(Url.Link("GetCamp", new { moniker = mappedCamp.Moniker }), mappedCamp);
                    }
                }
            }
            catch (Exception ex)    
            {
                return StatusCode(500, ex);
            }

            return BadRequest(ModelState);
        }

        [Route("{moniker}")]
        public async Task<ActionResult> Put(string moniker, CampModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var camp = await _campRepository.GetCampAsync(moniker);
                    if (camp == null) return NotFound();

                    _mapper.Map(model, camp);

                    if (await _campRepository.SaveChangesAsync())
                    {
                        return Ok(_mapper.Map<CampModel>(camp));
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return BadRequest(ModelState);
        }

        [Route("{moniker}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(string moniker)
        {
            try
            {
                var camp = await _campRepository.GetCampAsync(moniker);
                if (camp == null) return NotFound();

                _campRepository.DeleteCamp(camp);

                var saved = await _campRepository.SaveChangesAsync();
                if (saved)
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

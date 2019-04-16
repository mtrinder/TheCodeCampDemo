﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheCodeCamp.Data
{
  public class CampRepository : ICampRepository
  {
    private readonly CampContext _context;

    public CampRepository(CampContext context)
    {
      _context = context;
    }

    public void AddCamp(Camp camp)
    {
      _context.Camps.Add(camp);
    }

    public void AddTalk(Talk talk)
    {
      _context.Talks.Add(talk);
    }

    public void AddSpeaker(Speaker speaker)
    {
      _context.Speakers.Add(speaker);
    }

    public void DeleteCamp(Camp camp)
    {
      _context.Camps.Remove(camp);
    }

    public void DeleteTalk(Talk talk)
    {
      _context.Talks.Remove(talk);
    }

    public void DeleteSpeaker(Speaker speaker)
    {
      _context.Speakers.Remove(speaker);
    }

    public async Task<bool> SaveChangesAsync()
    {
      // Only return success if at least one row was changed
      return (await _context.SaveChangesAsync()) > 0;
    }

    public bool SaveChanges()
    {
        // Only return success if at least one row was changed
        return _context.SaveChanges() > 0;
    }

    public async Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
    {
      IQueryable<Camp> query = _context.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query
          .Include(c => c.Talks).ThenInclude(t => t.TalkSpeakers).ThenInclude(s => s.Speaker);
      }

      // Order It
      query = query.OrderByDescending(c => c.EventDate)
        .Where(c => c.EventDate == dateTime);

      return await query.ToArrayAsync();
    }

    public async Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
    {
      IQueryable<Camp> query = _context.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query
          .Include(c => c.Talks)
          .ThenInclude(t => t.TalkSpeakers).ThenInclude(s => s.Speaker);
      }

      // Order It
      query = query.OrderByDescending(c => c.EventDate);

      return await query.ToArrayAsync();
    }

    public async Task<Camp> GetCampAsync(string moniker, bool includeTalks = false)
    {
      IQueryable<Camp> query = _context.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query.Include(c => c.Talks).ThenInclude(t => t.TalkSpeakers).ThenInclude(s => s.Speaker);
      }

      // Query It
      query = query.Where(c => c.Moniker == moniker);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
    {
      IQueryable<Talk> query = _context.Talks;

      if (includeSpeakers)
      {
        query = query
          .Include(t => t.TalkSpeakers).ThenInclude(s => s.Speaker);
      }

      // Add Query
      query = query
        .Where(t => t.Camp.Moniker == moniker)
        .OrderByDescending(t => t.Title);

      return await query.ToArrayAsync();
    }

    public async Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false)
    {
      IQueryable<Talk> query = _context.Talks;

      if (includeSpeakers)
      {
        query = query
          .Include(t => t.TalkSpeakers).ThenInclude(s => s.Speaker);
      }

      // Add Query
      query = query
        .Where(t => t.TalkId == talkId && t.Camp.Moniker == moniker);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker)
    {
        throw new NotImplementedException();

    //    IQueryable<ICollection<TalkSpeakers>> query = _context.Talks
    //      .Where(t => t.Camp.Moniker == moniker)
    //      .Select(t => t.TalkSpeakers);

    //    var result = await query.ToArrayAsync();

    //    result.ToList().SelectMany()

    //    //.Where(s => s != null)
    //    //.OrderBy(s => s. .LastName)
    //    //.Distinct();

    //        //return await query.ToArrayAsync();
    }

    public async Task<Speaker[]> GetAllSpeakersAsync()
    {
      var query = _context.Speakers
        .OrderBy(t => t.LastName);

      return await query.ToArrayAsync();
    }


    public async Task<Speaker> GetSpeakerAsync(int speakerId)
    {
      var query = _context.Speakers
        .Where(t => t.SpeakerId == speakerId);

      return await query.FirstOrDefaultAsync();
    }
  }
}

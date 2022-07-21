using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using System.Collections.Generic;

namespace MusicLibrary.Controllers
{
    public class RecordsController : Controller
    {

      [HttpGet("/artists/{artistId}/records/new")]
      public ActionResult New(int artistId)
      {
        Artist artist = Artist.Find(artistId);
        return View(artist);
      }

      [HttpGet("/artists/{artistId}/records/{recordId}")]
      public ActionResult Show(int artistId, int recordId)
      {
        Record record = Record.Find(recordId);
        Artist artist = Artist.Find(artistId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("record", record);
        model.Add("artist", artist);
        return View(model);
      }

    }
}
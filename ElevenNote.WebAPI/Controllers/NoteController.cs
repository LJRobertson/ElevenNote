﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController
    {
        private NoteService CreateNoteSerivce()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }

        public IHttpActionResult Get()
        {
            NoteService noteService = CreateNoteSerivce();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }

        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteSerivce();

            if (!service.CreateNote(note))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            NoteService noteService = CreateNoteSerivce();
            var note = noteService.GetNoteById(id);
            return Ok(note);
        }

        public IHttpActionResult Put(NoteEdit note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteSerivce();

            if (!service.UpdateNote(note))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete (int id)
        {
            var service = CreateNoteSerivce();

            if (!service.DeleteNote(id))
                return InternalServerError();

            return Ok();
        }

    }
}

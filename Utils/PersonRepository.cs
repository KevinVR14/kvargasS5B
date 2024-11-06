using kvargasS5B.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kvargasS5B.Utils
{
    public class PersonRepository
    {
        string dbPath;
        private SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public PersonRepository(string path) 
        { 
            dbPath = path;
        }

        public void Init()
        {
            if (connection is not null)
                return;
            connection = new(dbPath);
            connection.CreateTable<Persona> ();
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre requerido");

                Persona persona = new() { Name = name };
                result = connection.Insert(persona);
                StatusMessage = string.Format("{0} record(s) added (Nombre: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return connection.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to list. Error: {1}", ex.Message);
            }
            return new List<Persona>();
        }

        public void EditPerson(int id, string name)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre requerido");

                Persona persona = new() {Id = id, Name = name };
                result = connection.Update(persona);
                StatusMessage = string.Format("{0} record(s) updated (Nombre: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update {0}. Error: {1}", name, ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            int result = 0;
            try
            {
                Init();

                Persona persona = new() { Id = id };
                result = connection.Delete(persona);
                StatusMessage = string.Format("{0} record(s) deleted (Id: {1})", result, id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete {0}. Error: {1}", id, ex.Message);
            }
        }
    }
}

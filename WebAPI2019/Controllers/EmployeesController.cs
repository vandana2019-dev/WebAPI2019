using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataLibrary;

namespace WebAPI2019.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET api/<controller>
        // The below is the default get
        //public IEnumerable<Employee> Get()
        //{
        //    using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}

        // Custom Method Name for HttpGet
        //[HttpGet]
        //public IEnumerable<Employee> LoadAllEmployees()
        //{
        //    using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}

        // Pass a Query String
        public HttpResponseMessage Get(string gender="All")
        {
            using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
            {
                switch(gender.ToLower())
                {
                    case "all": return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());

                    case "male": return Request.CreateResponse(HttpStatusCode.OK, 
                                    entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());

                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                         entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Value for gender must be All, Male or Female " + "Gender " + gender + " - is invalid");

                }
                 
            }
        }


        //public Employee Get(int id)
        //{
        //    // If an employee Id does not exist in the DB
        //    using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //    {
        //        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        //        return entity;
        //    }
        //}

        //public HttpResponseMessage Get(int id)
        //{
        //    // If an employee Id does not exist in the DB
        //    using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //    {
        //       var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        //        if(entity != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id =" + id.ToString() + " not found");
        //        }
        //    }
        //}

        // Use Custom Method Names
        [HttpGet]
        public HttpResponseMessage LoadEmployeeById(int id)
        {
            // If an employee Id does not exist in the DB
            using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id =" + id.ToString() + " not found");
                }
            }
        }

        // The below will return void
        //public void Post(Employee employee)
        //{
        //    using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //    {
        //        entities.Employees.Add(employee);
        //        entities.SaveChanges();
        //    }
        //}

        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    // The Header Location will be set to the new Employee Added
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.ID.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
               return  Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // The below Delete Method does not return anything, to return the Status Code write as below
        //public void Delete(int id)
        //{
        //    using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //    {
        //        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
        //    }
        //}

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
                {
                    var entity = entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Put is Update an existing entry
        //public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        //{
        //    try
        //    {
        //        using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //        {

        //            var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found to delete");
        //            }
        //            else
        //            {
        //                entity.FirstName = employee.FirstName;
        //                entity.LastName = employee.LastName;
        //                entity.Gender = employee.Gender;
        //                entity.Salary = employee.Salary;

        //                entities.SaveChanges();

        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        // https://localhost:44396/api/employees/?id=8&FirstName=MarkFN&LastName=HastingLN&Gender=Male&Salary=5000
        //public HttpResponseMessage Put(int id, [FromUri] Employee employee)
        //{
        //    try
        //    {
        //        using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
        //        {

        //            var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found to delete");
        //            }
        //            else
        //            {
        //                entity.FirstName = employee.FirstName;
        //                entity.LastName = employee.LastName;
        //                entity.Gender = employee.Gender;
        //                entity.Salary = employee.Salary;

        //                entities.SaveChanges();

        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        // https://localhost:44396/api/employees/?iFirstName=MarkFN1&LastName=HastingLN1&Gender=Male&Salary=50001
        // Set the id in the Body
        public HttpResponseMessage Put([FromBody] int id, [FromUri] Employee employee)
        {
            try
            {
                using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
                {

                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
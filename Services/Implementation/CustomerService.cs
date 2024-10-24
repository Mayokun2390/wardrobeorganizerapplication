using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Constant;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerInterface _customerInterface;
        private readonly IUserInterface _userInterface;
        private readonly IRoleInterface _roleInterface;
        private readonly IUnitOfWork _unitofwork;
        public CustomerService(ICustomerInterface customerInterface, IUserInterface userInterface, IRoleInterface roleInterface, IUnitOfWork unitofwork) 
        {
            _customerInterface = customerInterface;
            _userInterface = userInterface;
            _roleInterface = roleInterface;
            _unitofwork = unitofwork;
        }
        public async Task<Response<CustomerResponseModel>> CreateCustomer(CustomerRequestModel model)
        {
            var exist = await _customerInterface.GetCustomerByEmail(m => m.Email == model.Email);
            var passwordSort = BCrypt.Net.BCrypt.GenerateSalt();
            var PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password,  passwordSort);
            if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new Response<CustomerResponseModel>
                {
                    Message = "Invalid Email",
                    Status = false,
                };
            }
            if (exist == null)
            {
                var user = new User
                {
                    Email = model.Email,
                    PasswordHash = PasswordHash,
                    PasswordSort = passwordSort,
                };
                await _userInterface.CreateUser(user);

                if (model.PhoneNumber.Length != 11)
                {
                    return new Response<CustomerResponseModel>
                    {
                        Message = "Invalid Number",
                        Status = false,
                    };
                }

                var getPhoneNumber = await _customerInterface.GetCustomerByEmail(x => x.PhoneNumber == model.PhoneNumber);
                if (getPhoneNumber != null){
                    return new Response<CustomerResponseModel>{
                        Message = "Phonenumber already exist",
                        Status = false,  
                    };
                }

                
                var getRoles = await _roleInterface.GetBy(x => x.Name == RoleConstant.Customer);
                if (getRoles == null)
                {
                    return new Response<CustomerResponseModel>
                    {
                        Message = "Role not found",
                        Status = false,
                    };
                }
               
               var userroles = new UserRole{
                RoleId = getRoles.Id,
                UserId = user.Id,
               };

               user.UserRoles.Add(userroles);


                var customer = new Customer
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                };
                await _customerInterface.Create(customer);
                var getRole =await _roleInterface.GetBy(x => x.Name == RoleConstant.Customer);
                if (getRole == null){
                    return new Response<CustomerResponseModel>{
                        Message = "Role not found",
                        Status = false
                    };
                }

                var userRole = new UserRole{
                  UserId = customer.Id,
                  RoleId = getRole.Id,  
                };
                user.UserRoles.Add(userRole);

               return new Response<CustomerResponseModel>
               {
                    Message = "Customer Created Successfully",
                    Status = true,
                    Value = new CustomerResponseModel
                    {
                        Id = customer.Id,
                        Email = customer.Email,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                    }
               };
            }
            return new Response<CustomerResponseModel>
            {
                Message = "User already exist",
                Status = false,
                Value = null,
            };
        }

        public async Task<Response<CustomerResponseModel>> Delete(Guid id)
        {
            var cust = await _customerInterface.GetCustomerById(id);
            if (cust == null)
            {
                return new Response<CustomerResponseModel>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            _customerInterface.Delete(cust);
            _unitofwork.SaveChanges();
            return new Response<CustomerResponseModel>
            {
                Message = "Customer deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<CustomerResponseModel>> Get(string email)
        {
            var customer = await _customerInterface.GetCustomerByEmail(e => e.Email == email);
            if (customer == null)
            {
                return new  Response<CustomerResponseModel>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            var getCustomer = new CustomerResponseModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };
            return new Response<CustomerResponseModel>
            {
                Message = "Customer Exist",
                Status = true,
                Value = getCustomer,
            };
        }

        public async Task<Response<ICollection<CustomerResponseModel>>> GetAllCustomers()
        {
            var customer = await _customerInterface.GetAllCustomers();
            var getCustomers = customer.Select(x => new CustomerResponseModel{  
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            }).ToList();

            return new Response<ICollection<CustomerResponseModel>>
            {
                Value = getCustomers,
                Message = "List of Customers",
                Status = true,
            };
        }

        public async Task<Response<CustomerResponseModel>> GetById(Guid id)
        {
            var customer = await _customerInterface.GetCustomerById(id);
            if (customer == null)
            {
                return new  Response<CustomerResponseModel>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            var getCustomer = new CustomerResponseModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,

            };
            return new Response<CustomerResponseModel>
            {
                Message = "Customer Exist",
                Status = true,
                Value = getCustomer,
            };
        }

        public async Task<Response<CustomerResponseModel>> Update(CustomerRequestModel model, Guid id)
        {
            var customer = await _customerInterface.GetCustomerById(id);
            if (customer == null)
            {
                return new Response<CustomerResponseModel>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            var updateCustomer = new Customer();
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.PhoneNumber = model.PhoneNumber;
            _customerInterface.Update(updateCustomer);
            _unitofwork.SaveChanges();
            return new Response<CustomerResponseModel>
            {
                Message = "Customer Updated",
                Status = true,
            };
        }
    }
}
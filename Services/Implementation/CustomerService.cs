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
            var getExistCustomer = await _customerInterface.GetCustomerByEmail(x => x.Email == model.Email);
            if (getExistCustomer != null)
            {
                return new Response<CustomerResponseModel>
                {
                    Message = "Email already exist",
                    Status = false
                };
            }

            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, passwordSalt);

            var user = new User
            {
                UserName = $"{model.FirstName}\t{model.LastName}",
                Email = model.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash

            };
            await _userInterface.CreateUser(user);


            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var getRole = await _roleInterface.GetBy(x => x.Name == RoleConstant.Customer);
            if (getRole == null)
            {
                return new Response<CustomerResponseModel>
                {
                    Message = "Role not found",
                    Status = false
                };
            }

            var userRole = new UserRole
            {
                RoleId = getRole.Id,
                UserId = user.Id
            };
            user.UserRoles.Add(userRole);
            await _customerInterface.Create(customer);
            _unitofwork.SaveChanges();
            return new Response<CustomerResponseModel>
            {
                Message = "Registration Successful",
                Status = true
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
                return new Response<CustomerResponseModel>
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
            var getCustomers = customer.Select(x => new CustomerResponseModel
            {
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
                return new Response<CustomerResponseModel>
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
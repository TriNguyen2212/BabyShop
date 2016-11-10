using BabyShop.Data.Infrastructure;
using BabyShop.Data.Reponsitories;
using BabyShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetDefaultContact();
    }

    public class ContactDetailService : IContactDetailService
    {
        public IUnitOfWork _unitOfWork;
        public IContactDetailRepository _contactDetailRepository;

        public ContactDetailService(IUnitOfWork unitOfWork, IContactDetailRepository contactDetailRepository)
        {
            this._unitOfWork = unitOfWork;
            this._contactDetailRepository = contactDetailRepository;
        }

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }
    }
}
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
    internal class SizeSevice
    {
        public interface ISizeService
        {
            void Add(Size Size);

            void Update(Size Size);

            void Delete(int id);

            IEnumerable<Size> GetAll();

            IEnumerable<Size> GetAllPaging(int page, int pageSize, out int totalRow);

            Size GetById(int id);

            void SaveChanges();
        }

        public class SizeService : ISizeService
        {
            private ISizeRepository _SizeRepository;
            private IUnitOfWork _unitOfWork;

            public SizeService(ISizeRepository SizeRepository, IUnitOfWork unitOfWork)
            {
                this._SizeRepository = SizeRepository;
                this._unitOfWork = unitOfWork;
            }

            public void Add(Size Size)
            {
                _SizeRepository.Add(Size);
            }

            public void Delete(int id)
            {
                _SizeRepository.Delete(id);
            }

            public IEnumerable<Size> GetAll()
            {
                return _SizeRepository.GetAll(new string[] { "Product", "Size", "Size" });
            }

            public IEnumerable<Size> GetAllPaging(int page, int pageSize, out int totalRow)
            {
                return _SizeRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
            }

            public Size GetById(int id)
            {
                return _SizeRepository.GetSingleById(id);
            }

            public void SaveChanges()
            {
                _unitOfWork.Commit();
            }

            public void Update(Size Size)
            {
                _SizeRepository.Update(Size);
            }
        }
    }
}
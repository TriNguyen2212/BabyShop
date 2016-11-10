using BabyShop.Data.Infrastructure;
using BabyShop.Data.Reponsitories;
using BabyShop.Model.Models;
using System;
using System.Collections.Generic;

namespace BabyShop.Service
{
    public interface IColorService
    {
        Color Add(Color Color);

        void Update(Color Color);

        Color Delete(int id);

        IEnumerable<Color> GetAll();

        IEnumerable<Color> GetAll(string keyword);

        IEnumerable<Color> GetAllPaging(int page, int pageSize, out int totalRow);

        Color GetById(int id);

        void SaveChanges();
    }

    public class ColorService : IColorService
    {
        private IColorRepository _colorRepository;
        private IUnitOfWork _unitOfWork;

        public ColorService(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            this._colorRepository = colorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Color Add(Color Color)
        {
            return _colorRepository.Add(Color);
        }

        public Color Delete(int id)
        {
            return _colorRepository.Delete(id);
        }

        public IEnumerable<Color> GetAll()
        {
            return _colorRepository.GetAll();
        }

        public IEnumerable<Color> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _colorRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _colorRepository.GetAll();
        }

        public IEnumerable<Color> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _colorRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Color GetById(int id)
        {
            return _colorRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Color Color)
        {
            _colorRepository.Update(Color);
        }
    }
}
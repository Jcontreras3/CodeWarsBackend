using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWarsBackend.Models;
using CodeWarsBackend.Models.DTO;
using CodeWarsBackend.Services.Context;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace CodeWarsBackend.Services
{
    public class KataService : ControllerBase
    {
        private readonly DataContext _context;

        public  KataService (DataContext context){
            _context = context;
        }
         public bool AddKataItem( KataModel newKataItem){
           _context.Add(newKataItem);
           return _context.SaveChanges() != 0;
        }

        public IEnumerable<KataModel> GetAllKataItems(){
            return _context.KataInfo;
        }

        public bool UpdateKataItem(KataModel KataUpdate){
            _context.Update<KataModel>(KataUpdate);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<KataModel> GetKataByCompleted(){
            return _context.KataInfo.Where(item => item.IsCompleted);
        }

        public IEnumerable<KataModel> GetKataByPending(){
            return _context.KataInfo.Where(item => !item.IsCompleted);
        }

         public IEnumerable<KataModel> GetItemsByUserAssigned(string userAssigned){
            return _context.KataInfo.Where(item => item.UserAssigned == userAssigned);
        }

        public IEnumerable<KataModel> GetRandomKataItems(){
            
            int count = dbContext.KataInfo.Count();
            return count;    
        }



    }
}
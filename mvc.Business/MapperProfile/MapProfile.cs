using AutoMapper;
using bookShop.Dtos.Requests;
using bookShop.Dtos.Responses;
using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.MapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book,BookListResponse>();
            CreateMap<AddBookRequest, Book>();
            //addcategoryrequest ->category  
            CreateMap<AddCategoryRequest, Category>();
            CreateMap<Category,CategoryListResponse>();

            CreateMap<AddPublisherRequest, Publisher>();
            CreateMap<Publisher, PublisherListResponse>();

            CreateMap<AddWriterRequest, Writer>();
            CreateMap<Writer, WriterListResponse>();

            CreateMap<AddUserRequest, User>();
            CreateMap<Book, UpdateBookResponse>();
            CreateMap<Publisher,UpdatePublisherResponse>();
            CreateMap<Category, UpdateCategoryResponse>();
            CreateMap<Writer, UpdateWriterResponse>();
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using BlogDomain.Entities;
using BlogDomain.Requests;
using BlogDomain.Responses;
using Domain.Responses;
using Infrastructure.Mapping;
using System.Net;

namespace Infrastructure.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlogService(IBlogRepository blogRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = blogRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<CustomDataResponse<BlogCreatedResponse>> CreateAsync(BlogCreateRequest request)
    {
        Blog blog = _mapper.Map<Blog>(request);

        await _repository.CreateAsync(blog);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<BlogCreatedResponse>(blog);

        return response.ToCustomDataResponse(true, HttpStatusCode.Created);
    }

    public async Task<CustomApiResponse> DeleteAsync(int id)
    {
        _repository.Delete(await FindBlogById(id));
        await _unitOfWork.CommitAsync();

        return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.NoContent);
    }

    public async Task<CustomDataResponse<BlogIndexResponse>> GetAllAsync()
    {
        IEnumerable<Blog> blogs = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<BlogIndexResponse>>(blogs);

        return response.ToCustomDataListResponse(true);
    }

    public async Task<CustomDataResponse<BlogShowResponse>> GetByIdAsync(int id)
    {
        var response = _mapper.Map<BlogShowResponse>(await FindBlogById(id));

        return response.ToCustomDataResponse(true);
    }

    public async Task<CustomApiResponse> UpdateAsync(int id, BlogUpdateRequest request)
    {

        Blog blog = await FindBlogById(id);

        blog.Title = request.Title;
        blog.Description = request.Description;
        blog.Content = request.Content;

        _repository.Update(blog);
        await _unitOfWork.CommitAsync();

        return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.OK);
    }

    private async Task<Blog> FindBlogById(int id)
    {
        Blog? blog = await _repository.GetByIdAsync(id);
        if (blog == null)
        {
            // TODO: Exception handling eklenecek
            throw new Exception("Blog not found");
        }
        return blog;
    }
}

using Api.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Mapping;
using System.Net;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomApiResponse> CreateAsync(UserCreateRequest request)
    {
        User user = _mapper.Map<User>(request);

        await _repository.CreateAsync(user);
        await _unitOfWork.CommitAsync();

        return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.Created);
    }

    public async Task<CustomApiResponse> DeleteAsync(string username)
    {
        _repository.Delete(await GetByUsernameAsync(username));
        await _unitOfWork.CommitAsync();

        return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.NoContent);
    }

    public async Task<CustomDataResponse<UserShowResponse>> GetAsync(string username)
    {
        var response = _mapper.Map<UserShowResponse>(await GetByUsernameAsync(username));

        return response.ToCustomDataResponse(true);
    }

    public CustomApiResponse Update(UserUpdateRequest request)
    {
        User user = _mapper.Map<User>(request);

        _repository.Update(user);
        _unitOfWork.Commit();

        return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.NoContent);
    }

    private async Task<User> GetByUsernameAsync(string username)
    {
        User user = await _repository.GetByUsernameAsync(username);

        // TODO: Exception handle edilecek
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return user;
    }
}

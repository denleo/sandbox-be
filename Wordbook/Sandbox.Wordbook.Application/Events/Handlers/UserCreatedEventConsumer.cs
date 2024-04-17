using MassTransit;
using Microsoft.Extensions.Logging;
using Sandbox.Contracts.Events.User;
using Sandbox.Wordbook.Domain;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Application.Events.Handlers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventConsumer> _logger;
    private readonly IUnitOfWork _uow;
    private readonly IWordbookUserRepository _userRepository;

    public UserCreatedEventConsumer(
        IWordbookUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserCreatedEventConsumer> logger)
    {
        _userRepository = userRepository;
        _uow = unitOfWork;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var isUserExists = await _userRepository.IsUserExists(context.Message.FirebaseId);

        if (!isUserExists)
        {
            var wordbookUser = new WordbookUser
            {
                Id = Guid.NewGuid(),
                FirebaseId = context.Message.FirebaseId,
                Email = context.Message.Email,
                FullName = context.Message.FullName
            };

            _userRepository.CreateUser(wordbookUser);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("[Wordbook] New user was created: {fullname} ({id})",
                wordbookUser.FullName,
                wordbookUser.Id);
        }
    }
}
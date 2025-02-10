using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TallerAPI.Core.Entities;
using TallerAPI.Core.Repositories;

namespace TallerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IUserRepository userRepository, ILogger<UsersController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a user by their username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A greeting if the user is found, or a not found message.</returns>
    /// <response code="200">Returns a greeting message with the user's name.</response>
    /// <response code="404">If the user is not found.</response>
    /// <response code="500">If an error occurs while processing the request.</response>
    [HttpGet("{username}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUser(string username)
    {
        if (string.IsNullOrEmpty(username) || username.Length > 50)
        {
            logger.LogError($"No username provided.");
            return BadRequest("Username is required");
        }
        
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
        {
            logger.LogError("Invalid username provided - {Username}", username);
            return BadRequest("Username can only contain letters, numbers, and underscores.");
        }
        
        try
        {
            var user = await userRepository.GetUserByUsernameAsync(username);

            if (user is null)
            {
                logger.LogError("User {User} not found.", username);
                return NotFound("User not found.");
            }
            
            logger.LogInformation("User {User} was found.", username);
            return Ok($"Hello, {HttpUtility.HtmlEncode(user.Name)}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting user {Username}.", username);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    
    /// <summary>
    /// Retrieves all users from the database.
    /// </summary>
    /// <returns>A list of all users.</returns>
    /// <response code="200">Returns the list of users.</response>
    /// <response code="500">If an error occurs while processing the request.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await userRepository.GetAllUsersAsync();
            
            logger.LogInformation("All users were retrieved.");
            return Ok(users);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting all users.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
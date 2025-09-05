
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost("auth")]
    public IActionResult Login([FromBody] Login login)
    {

        if (login.Username == "admin" && login.Password == "uninove123")
        {
            return Ok(new { message = "Login bem-sucedido!"});
        }
         if (login.Username == "admin" || login.Password == "")
        {
            return BadRequest(new { message = "Esqueceu sua senha? Puts.." });
        }
        
        return Unauthorized(new { message = "Usuário ou senha inválidos." });
    }
}
document.addEventListener('DOMContentLoaded', () => {

    const LoginFormulario = document.getElementById('login-form');
    const usuario = document.getElementById('username');
    const senha = document.getElementById('password');
    const menssagemAlert = document.getElementById('login-message');


    LoginFormulario.addEventListener('submit', async (event) => {
        event.preventDefault();

        const username = usuario.value;
        const password = senha.value;

        menssagemAlert.textContent = '';
        menssagemAlert.style.color = 'red';

        const loginData = {
            username: username,
            password: password
        };

   
        try {

            const response = await fetch('http://localhost:5049/api/Login/auth', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginData) 
            });

    
            if (response.ok) { 
                const result = await response.json();
                
                console.log('Login bem-sucedido:', result);
                menssagemAlert.textContent = 'Login realizado com sucesso!';
                menssagemAlert.style.color = 'green';

                window.location.href = './dashboard.html';

            } else {
                const errorData = await response.json();
                menssagemAlert.textContent = `Erro no login: ${errorData.message || 'Credenciais inválidas.'}`;
            }

        } catch (error) {
            console.error(`Erro de rede: ${errorData.message}`, error);
            menssagemAlert.textContent = 'Não foi possível conectar ao servidor. Tente novamente mais tarde.';
        }
    });
});
<template>
    <div class="container">
        <div v-if="isLogin" class="form-card">
            <h2>Login</h2>
            <form @submit.prevent="handleLogin">
                <div class="form-group">
                    <label>Nome de Usuário</label>
                    <input type="text" v-model="nome" required placeholder="Digite seu usuário">
                </div>
                <div class="form-group">
                    <label>Senha</label>
                    <input type="password" v-model="senha" required placeholder="Digite sua senha">
                </div>
                <div v-if="usuarioJaExiste" class="error-message">
                    Nome de usuário já existe!
                </div>
                <button type="submit" class="btn btn-login">Entrar</button>
            </form>
            <p class="toggle-text">
                Não tem uma conta? <a @click="toggleView">Cadastre-se</a>
            </p>
        </div>

        <!-- Cadastro -->
        <div v-else class="form-card">
            <h2>Cadastro</h2>
            <form @submit.prevent="handleCadastro">
                <div class="form-group">
                    <label>Nome de Usuário</label>
                    <input type="text" v-model="nome" required placeholder="Escolha um usuário">
                </div>
                <div class="form-group">
                    <label>Senha</label>
                    <input type="password" v-model="senha" required placeholder="Crie uma senha">
                </div>
                <div class="form-group">
                    <label>Confirmar Senha</label>
                    <input type="password" v-model="senhaConfirmar" required placeholder="Confirme sua senha">
                </div>
                <button type="submit" class="btn btn-cadastro">Criar Conta</button>
            </form>
            <p class="toggle-text">
                Já tem uma conta? <a @click="toggleView">Faça Login</a>
            </p>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            isLogin: true,
            usuarios: [],
            nome: "",
            senha: "",
            senhaConfirmar: "",
            usuarioJaExiste: false
        }
    },
    methods: {
        toggleView() {
            this.isLogin = !this.isLogin;
            this.nome = "";
            this.senha = "";
            this.senhaConfirmar = "";
        },
        handleLogin() {
            if (!this.nome || !this.senha) {
                alert("Por favor, preencha todos os campos.");
                return;
            }
            console.log("Tentativa de Login com:", {
                usuario: this.nome,
                senha: this.senha
            });

            const loginData = {
                nomeUsuario: this.nome,
                senha: this.senha
            };

            this.$http.post('http://localhost:5000/api/usuario/login', loginData)
                .then(response => {
                    const dadosDoUsuario = response.body.usuario;

                    localStorage.setItem('user-token', 'true');

                    localStorage.setItem('isAdmin', dadosDoUsuario.isAdmin);

                    this.$router.push('/professores');

                })
                .catch(error => {
                    console.error("Erro no login:", error);
                    alert("Erro no login: " + error.body);
                });
        },
        handleCadastro() {
            // Lógica para o cadastroadsadsa
            if (!this.nome || !this.senha || !this.senhaConfirmar) {
                alert("Por favor, preencha todos os campos.");
                return;
            }
            if (this.senha !== this.senhaConfirmar) {
                alert("As senhas não coincidem!");
                return;
            }
            console.log("Tentativa de Cadastro com:", {
                usuario: this.nome,
                senha: this.senha
            });

            const dadosUsuario = {
                nomeUsuario: this.nome,
                senha: this.senha
            };
            this.$http.post('http://localhost:5000/api/usuario', dadosUsuario, {})
                .then(response => {
                    // Sucesso!
                    console.log('Usuário cadastrado:', response.body);
                    alert("Usuário cadastrado com sucesso! Por favor, faça o login.");
                    this.toggleView(); // Alterna para a tela de login
                })
                .catch(error => {
                    // Erro! O '.body' do objeto de erro contém a mensagem do seu backend.
                    // Ex: "Nome de usuário já existe."
                    console.error("Erro no cadastro:", error);
                    alert("Erro no cadastro: " + error.body);
                });

        }
    }
}
</script>

<style scoped>
.container {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 2rem;
    min-height: 80vh;
}

.form-card {
    width: 100%;
    max-width: 450px;
    padding: 2rem;
    background-color: white;
    border: 1.5px solid black;
    border-radius: 8px;
    text-align: center;
}

h2 {
    margin-bottom: 1.5rem;
    font-size: 1.75rem;
    color: #333;
}

.form-group {
    margin-bottom: 1.5rem;
    text-align: left;
}

label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: 600;
    color: #555;
}

input[type="text"],
input[type="password"] {
    width: 100%;
    box-sizing: border-box;
    padding: 10px;
    border: 1.5px solid black;
    border-radius: 5px;
    font-size: 1.1em;
    font-family: "Comic Sans MS", cursive, sans-serif;
}

.btn {
    width: 100%;
    padding: 10px;
    cursor: pointer;
    border: 1.5px solid black;
    color: aliceblue;
    font-weight: bold;
    font-size: 1.1em;
    border-radius: 5px;
    border-bottom: 3px solid black;
    font-family: "Comic Sans MS", cursive, sans-serif;
    transition: all 0.1s ease-in-out;
}

.btn:hover {
    text-shadow: 1px 1px black;
    border-bottom-width: 1.5px;
    margin-top: 1.5px;
}

.btn-login {
    background-color: rgb(49, 117, 235);
    /* Azul */
}

.btn-cadastro {
    background-color: rgb(83, 204, 164);
    /* Verde */
}

.toggle-text {
    margin-top: 1.5rem;
    font-size: 0.9rem;
}

.toggle-text a {
    color: rgb(73, 73, 255);
    font-weight: 600;
    cursor: pointer;
    text-decoration: none;
}

.toggle-text a:hover {
    text-decoration: underline;
}
</style>

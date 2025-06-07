<template>
  <div>
    <Titulo texto="Cadastrar Novo Administrador" :boolVoltar="true" />
    <div class="form-container">
      <div class="form-group">
        <label for="username">Nome de Usuário</label>
        <input type="text" v-model="nomeUsuario" placeholder="novo.admin" class="form-input">
      </div>
      <div class="form-group">
        <label for="password">Senha</label>
        <input type="password" v-model="senha" placeholder="••••••••" class="form-input">
      </div>
      <button @click="cadastrarAdmin" class="btn btn">Cadastrar Administrador</button>
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
      <div v-if="successMessage" class="success-message">{{ successMessage }}</div>
    </div>
  </div>
</template>

<script>
import Titulo from '../_share/Titulo-A.vue';

export default {
  name: 'AdminRegister',
  components: {
    Titulo
  },
  data() {
    return {
      nomeUsuario: '',
      senha: '',
      errorMessage: '',
      successMessage: ''
    }
  },
  methods: {
    cadastrarAdmin() {
      // Limpa mensagens anteriores
      this.errorMessage = '';
      this.successMessage = '';

      if (!this.nomeUsuario || !this.senha) {
        this.errorMessage = "Nome de usuário e senha são obrigatórios.";
        return;
      }

      const novoAdmin = {
        nomeUsuario: this.nomeUsuario,
        senha: this.senha,
        isAdmin: true
      };

      // Faz a chamada para o NOVO endpoint /api/usuario/admin
      this.$http.post('http://localhost:5000/api/usuario', novoAdmin)
        .then(response => {
          this.successMessage = `Administrador '${response.body.nomeUsuario}' cadastrado com sucesso!`;
          // Limpa os campos após o sucesso
          this.nomeUsuario = '';
          this.senha = '';
        })
        .catch(error => {
          console.error("Erro ao cadastrar admin:", error);
          this.errorMessage = error.body || "Falha ao cadastrar administrador. O nome de usuário pode já existir.";
        });
    }
  }
}
</script>

<style scoped>
.form-container {
    padding: 2rem;
    background-color: white;
    border-radius: 8px;
    border: 1.5px solid black;
    max-width: 500px;
    margin: 2rem auto;
}
.form-group {
    margin-bottom: 1.5rem;
}
label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: bold;
}
.form-input {
    width: 100%;
    padding: 0.75rem;
    border-radius: 4px;
    border: 1px solid #ccc;
    box-sizing: border-box; 
}
.btn {
    background-color: #2563EB;
}
.error-message {
    color: #d9534f;
    margin-top: 1rem;
    text-align: center;
}
.success-message {
    color: #5cb85c;
    margin-top: 1rem;
    text-align: center;
}
</style>

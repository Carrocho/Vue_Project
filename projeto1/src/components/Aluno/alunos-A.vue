<template>
  <div>
    <Titulo
      :texto="professorId != undefined ? 'Professor(a): ' + professor.nome + ' ' + professor.sobrenome : 'Todos os alunos'"
      v-if="professor.sobrenome != undefined" :boolVoltar="true" />
    <Titulo :texto="professorId != undefined ? 'Professor(a): ' + professor.nome : 'Todos os alunos'" :boolVoltar="true"
      v-else />

    <img v-if="professor.foto" :src="`http://localhost:5000${professor.foto}`" alt="Foto do professor"
      style="width: 250px; height: 250px; object-fit: cover;">
    <div class="divInput" v-if="professorId != undefined">
      <h3>CPF Professor: {{ professor.cpf }}</h3>
      <input type="text" placeholder="Nome do Aluno" v-model="nome" v-on:keyup.enter="adicionarAluno()">
      <button class="btn btnInput" @click="adicionarAluno(nome)">Adicionar</button>
      <input type="text" placeholder="CPF do aluno" v-model="cpfBase"
        @input="cpfBase = formatarCPF($event.target.value)" maxlength="14" />
      <div v-if="cpfInvalido" style="color: red; margin-bottom: 10px; display: inline;">
        CPF inválido!
      </div>
      <div v-if="cpfExistente" style="color: red; margin-bottom: 10px; display: inline;">
        CPF já existe!
      </div>
    </div>
    <div>
      <table>
        <thead>
          <th>Foto</th>
          <th>Matrícula</th>
          <th>CPF</th>
          <th>Nome</th>
          <th>Opções</th>
        </thead>
        <tbody>
          <tr v-for="(aluno, index) in alunos" :key="index">
            <td class="colPequeno" style="text-align: center; width: 20%;">
              <img v-if="aluno.foto" :src="`http://localhost:5000/api/aluno/${aluno.id}/foto`" alt="Foto do aluno"
                style="width: 80px; height: 80px; object-fit: cover;">
            </td>
            <td class="colPequeno" style="text-align: center; width: 20%;">{{ aluno.id }}</td>
            <td class="colPequeno" style="text-align: center; width: 20%;">{{ aluno.cpf }}</td>
            <router-link v-bind:to="`/alunoDetalhe/${aluno.id}`" tag="td" style="cursor: pointer;">
              {{ aluno.nome }} {{ aluno.sobrenome }}
            </router-link>
            <td class="colPequeno" style="text-align: center; width: 20%;">
              <button class="btn btnDanger" @click="removerAluno(aluno)">Remover</button>
            </td>
          </tr>
        </tbody>
        <tfoot v-if="!alunos.length">
          <tr>
            <td colspan="5">
              <h4>Nenhum aluno encontrado</h4>
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>

<script>

import Titulo from '../_share/Titulo-A';

export default {
  components: {
    Titulo
  },
  data() {
    return {
      nome: '',
      alunos: [],
      professorId: this.$route.params.prof_id,
      professor: {},
      cpfBase: "",
      cpfInvalido: false,
      cpfExistente: false
    }
  },
  created() {
    if (this.professorId) {
      this.carregarProfessores();
      this.$http.get(`http://localhost:5000/api/aluno/ByProfessor/${this.professorId}`)
        .then(res => res.json())
        .then(alunos => this.alunos = alunos)
    } else {
      this.$http.get('http://localhost:5000/api/aluno')
        .then(res => res.json())
        .then(alunos => this.alunos = alunos)
    }

  },
  props: {

  },
  methods: {
    carregarProfessores() {
      this.$http
        .get('http://localhost:5000/api/professor/' + this.professorId)
        .then(res => res.json())
        .then(professor => {
          this.professor = professor
        })
    },
    adicionarAluno() {
      this.cpfInvalido = false;
      this.cpfExistente = false;

      let _aluno = {
        nome: this.nome,
        sobrenome: '',
        dataNasc: '',
        professorId: this.professorId,
        cpf: this.cpfBase
      };
      this.$http.post('http://localhost:5000/api/aluno', _aluno)
        .then(res => {
          if (res.ok) {
            return res.json();
          } else {
            return res.text().then(textErrorMessage => {
              const error = new Error(textErrorMessage || `Erro HTTP ${res.status}`);
              error.status = res.status;
              error.body = textErrorMessage;
              throw error;
            });
          }
        })
        .then(alunoRetornado => {
          this.alunos.push(alunoRetornado);
          this.nome = '';
        })
        .catch(error => {
        console.error("Erro ao adicionar aluno:", error);

        let mensagemDeErroDoServidor = "";
        let statusDoErro = null;

        if (error.body) {
          mensagemDeErroDoServidor = error.body;
        }

        if (error.status) {
          statusDoErro = error.status;
        }

        if (statusDoErro === 400 && mensagemDeErroDoServidor.toLowerCase().includes('erro 02')) {
          this.cpfExistente = true; 
        } 
        if (statusDoErro === 400 && mensagemDeErroDoServidor.toLowerCase().includes('erro 01')) {
          this.cpfInvalido = true; 
        } 
      });
    },
    removerAluno(aluno) {
      this.$http.delete(`http://localhost:5000/api/aluno/${aluno.id}`).then(() => {
        let indice = this.alunos.indexOf(aluno);
        this.alunos.splice(indice, 1);
      })
    },
    calcularCPF(cpf) {
      console.log(cpf)
      cpf = cpf.replace(/\D/g, '');
      console.log(cpf)

      if (cpf.length !== 11) {
        return false;
      }

      const numeros = cpf.split('').map(Number);

      console.log(numeros)
      //primeiro dígito
      let soma = 0;
      for (let i = 0; i < 9; i++) {
        soma += numeros[i] * (10 - i);
      }
      let resto = soma % 11;
      const d1 = 11 - resto >= 10 ? 0 : 11 - resto;

      //segundo dígito
      soma = 0;
      for (let i = 0; i < 9; i++) {
        soma += numeros[i] * (11 - i);
      }
      soma += d1 * 2;
      resto = soma % 11;
      const d2 = 11 - resto >= 10 ? 0 : 11 - resto;

      return ((numeros[9] == d1) && (numeros[10] == d2));
    },
    formatarCPF(valor) {
      // Remove tudo que não for número
      valor = valor.replace(/\D/g, '');

      // Limita a 11 dígitos
      valor = valor.substring(0, 11);

      // Aplica a máscara: 000.000.000-00
      if (valor.length > 9) {
        valor = valor.replace(/(\d{3})(\d{3})(\d{3})(\d{1,2})/, "$1.$2.$3-$4");
      } else if (valor.length > 6) {
        valor = valor.replace(/(\d{3})(\d{3})(\d{1,3})/, "$1.$2.$3");
      } else if (valor.length > 3) {
        valor = valor.replace(/(\d{3})(\d{1,3})/, "$1.$2");
      }
      return valor;
    }
  },

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
input {
  width: calc(100% - 200px);
  border: 0;
  padding: 10px;
  font-size: 1.1em;
  color: rgb(66, 66, 66);
  margin-right: 10px;
  display: inline;
}

.btnInput {
  width: 160px;
  background-color: lightgreen;
  border: 1.5px solid black;
  padding: 10px;
  font-size: 1.1em;
  display: inline;
}

.btnInput:hover {
  border: 1.5px solid black;
  margin: 0;
}

.divInput {
  padding-bottom: 8px;
}
</style>

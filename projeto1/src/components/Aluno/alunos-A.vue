<template>
  <div>
    <Titulo :texto=" professorId != undefined ? 'Professor(a): ' + professor.nome : 'Todos os alunos'" v-if="professor.sobrenome != undefined" />
    <Titulo :texto="professorId != undefined ? 'Professor(a): ' + professor.nome : 'Todos os alunos'" v-else/>
    <div class="divInput" v-if="professorId != undefined">
      <input type="text" placeholder="Nome do Aluno" v-model="nome" v-on:keyup.enter="adicionarAluno()">
      <button class="btn btnInput" @click="adicionarAluno(nome)">Adicionar</button>
    </div>
    <table>
      <thead>
        <th>Matrícula</th>
        <th>Nome</th>
        <th>Opções</th>
      </thead>
      <tbody>
        <tr v-for="(aluno, index) in alunos" :key="index">
          <td class="colPequeno" style="text-align: center; width: 20%;">{{ aluno.id }}</td>
          <router-link v-bind:to="`/alunoDetalhe/${aluno.id}`" tag="td" style="cursor: pointer;"> 
            {{ aluno.nome }} {{ aluno.sobrenome}}
          </router-link>
          <td class="colPequeno" style="text-align: center; width: 20%;">
            <button class="btn btnDanger" @click="removerAluno(aluno)">Remover</button>
          </td>
        </tr>
      </tbody>
      <tfoot v-if="!alunos.length"> Nenhum aluno encontrado </tfoot>
    </table>
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
      professor: {}
    }
  },
  created() {
    if(this.professorId){
      this.carregarProfessores();
      this.$http.get('http://localhost:3000/alunos?professor.id=' + this.professorId)
      .then(res => res.json())
      .then(alunos => this.alunos = alunos)
    } else {
      this.$http.get('http://localhost:3000/alunos')
      .then(res => res.json())
      .then(alunos => this.alunos = alunos)
    }
    
  },
  props: {

  },
  methods: {
    carregarProfessores(){
        this.$http
        .get('http://localhost:3000/professores/' + this.professorId)
        .then(res => res.json())
        .then(professor => {
            this.professor = professor
        })
    },
    adicionarAluno() {
      let _aluno = {
        nome: this.nome,
        sobrenome: '',
        professor: {
          id: this.professor.id,
          nome: this.professor.nome,
          sobrenome: this.professor.sobrenome
        }
      };
      this.$http.post('http://localhost:3000/alunos', _aluno).then(res => res.json()).then(alunoRetornado => {
        this.alunos.push(alunoRetornado);
        this.nome = '';
      });
    },
    removerAluno(aluno) {
      this.$http.delete(`http://localhost:3000/alunos/${aluno.id}`).then(() => {
        let indice = this.alunos.indexOf(aluno);
        this.alunos.splice(indice, 1);
      })
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

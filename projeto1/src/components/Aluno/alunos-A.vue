<template>
  <div>
    <titulo texto="Aluno"/>
    <div class="divInput">
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
          <td>{{aluno.id}}</td>
          <td v-if="aluno.sobrenome.length">{{aluno.nome}} {{ aluno.sobrenome[0] }}.</td>
          <td v-else>{{aluno.nome}}</td>
          <td>
            <button class="btn btnDanger" @click="removerAluno(aluno)">Remover</button>
          </td>
        </tr>
      </tbody>
      <tfoot v-if="!alunos.length" > Nenhum aluno encontrado </tfoot>
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
      titulo: 'Aluno',
      nome: '',
      alunos: [
      ],
    }
  },
  created(){
    this.$http.get('http://localhost:3000/alunos').then(res => res.json()).then(alunos => this.alunos = alunos)
  },
  props: {

  },
  methods: {
    adicionarAluno() {
      let _aluno = {
          nome: this.nome,
          sobrenome: ''
        };
      this.$http.post('http://localhost:3000/alunos', _aluno).then(res => res.json()).then(alunoRetornado => {
        this.alunos.push(alunoRetornado);
        this.nome = '';
      });  
    },
    removerAluno(aluno){
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
  input{
    border: 0;
    padding: 8px;
    font-size: 1.1em;
    color: rgb(66, 66, 66);
    margin-right: 10px;
    display: inline;
  }

  .btnInput{
    width: 150px;
    background-color: lightgreen;
    border: 1.5px solid black;
    padding: 8px;
    font-size: 1.1em;
    display: inline;
  }

  .btnInput:hover{
    border: 1.5px solid black;
    margin: 0;
  }

  .divInput {
    padding-bottom: 8px;
  }
</style>

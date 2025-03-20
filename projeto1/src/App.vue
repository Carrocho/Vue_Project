<template>
  <div id="app">
    <alunos />
  </div>
</template>

<script>
import alunos from './components/Aluno/alunos-A.vue'

export default {
  name: 'App',
  components: {
    alunos
  }
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Comic+Neue&display=swap');

#app {

}

body{
  background-color: lightgray;
  font-family: 'Comic Sans MS', cursive, sans-serif;
  display: grid;
  justify-items: center;
}
body, html{
  margin: 0px;
  height: 100%;
}
table {
  margin: 0px;
  padding: 0px;
  list-style-type: none;
  width: 100%;
}
table tr td { 
  padding: 10px;
  font-size: 1.1em;
  background-color: rgb(211, 255, 241);
  margin-bottom: 20px;
}
table thead th{
  background-color: rgb(100, 253, 202);
  font-size: 1.2em;
  padding: 10px 0px;
  text-align: center !important;
}
.colPequeno{
  width: 5%;
}
.btnDanger{
  background-color: rgb(252, 103, 86);
}
.btn{
  width: 100%;
  padding: 8px 8px;
  cursor: pointer;
  border: 1.5px solid black;
  color: antiquewhite;
  font-weight: bold;
  font-size: 0.7em;
  border-radius: 5px;
  border-bottom: 3px solid black;
}
.btn:hover{
  text-shadow: 1px 1px black;
  border-bottom: 1px solid black;
  margin-top: 2px;
}
</style>

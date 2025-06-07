<template>
  <div id="app">
    <!-- Use v-if para mostrar o Nav somente se o usuário estiver logado -->
    <Nav v-if="usuarioLogado"></Nav>

    <div class="margemPrincipal">
      <router-view :key="$route.fullPath"></router-view>
    </div>
  </div>
</template>

<script>
import Nav from "./components/_nav/Nav-A.vue";

export default {
  name: "App",
  components: {
    Nav,
  },
  data() {
    return {
      usuarioLogado: false
    }
  },
  watch: {
    '$route'() {
      this.verificarLogin();
    }
  },
  created() {
    this.verificarLogin();
  },
  methods: {
    verificarLogin() {
      if (localStorage.getItem('user-token')) {
        this.usuarioLogado = true;
      } else {
        this.usuarioLogado = false;
      }
    }
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Comic+Neue&display=swap");

.none {
   display: none;
}

#app {
  width: 100%;
}

.margemPrincipal {
  width: 50%;
  margin: auto;
}

body {
  background-color: lightgray;
  font-family: "Comic Sans MS", cursive, sans-serif;
  display: grid;
  justify-items: center;
}

body,
html {
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
  text-align: center;
}

table thead th {
  background-color: rgb(83, 204, 164);
  font-size: 1.2em;
  padding: 10px 0px;
  text-align: center;
}

.colPequeno {
  width: 5%;
  text-align: right;
  background-color: rgb(100, 253, 202);
  font-weight: bold;
}

.btnDanger {
  background-color: rgb(252, 103, 86);
}

.btn {
  width: 100%;
  padding: 8px 8px;
  cursor: pointer;
  border: 1.5px solid black;
  color: aliceblue;
  font-weight: bold;
  font-size: 1em;
  border-radius: 5px;
  border-bottom: 3px solid black;
}

.btn:hover {
  text-shadow: 1px 1px black;
  border-bottom: 1.5px solid black;
  margin-top: 2px;
}

.btnFoto {
    background-color: rgb(114, 114, 252);
    width: 70px;
    font-size: 0.52em;
    border: 1.5px solid black;
  
}

.btnFoto:hover {
  text-shadow: 1px 1px black;
  border: 1.5px solid black;
  margin-top: 0px;
}
</style>

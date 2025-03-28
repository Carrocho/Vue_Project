<template>
    <div>
        <Titulo :texto="`Aluno: ${aluno.nome} `" :boolVoltar="!visualizando"> 
            <button v-show="visualizando" class="btn btnEditar" @click="editar()">Editar</button>
        </Titulo>
        
        <table>
            <tbody>
                <tr>
                    <td class="colPequeno">Matrícula:</td>
                    <td>
                        <label>{{ aluno.id }}</label>
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Nome:</td>
                    <td>
                        <label v-if="visualizando">{{ aluno.nome }}</label>
                        <input v-else v-model="aluno.nome" type="text">
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Sobrenome:</td>
                    <td>
                        <label v-if="visualizando">{{ aluno.sobrenome }}</label>
                        <input v-else v-model="aluno.sobrenome" type="text">
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Data de Nascimento:</td>
                    <td>
                        <label v-if="visualizando">{{ aluno.dataNasc }}</label>
                        <input v-else v-model="aluno.dataNasc" type="text">
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Professor:</td>
                    <td>
                        <label v-if="visualizando">{{ aluno.professor.nome }}</label>
                        <select v-else v-model="aluno.professor">
                            <option v-for="(professor, index) in professores" :key="index" v-bind:value="professor">
                                {{ professor.nome }}
                            </option>
                        </select>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="margin-top: 10px;">
            <div v-if="!visualizando">
                <button class="btn btnCancelar" @click="cancelar()">Cancelar</button>
                <button class="btn btnSalvar" @click="salvar(aluno)">Salvar</button>
            </div>
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
            professores: [],
            aluno: {},
            idAluno: this.$route.params.aluno_id,
            visualizando: true
        }
    },
    created() {
        this.$http.get('http://localhost:3000/alunos/' + this.idAluno)
            .then(res => res.json())
            .then(aluno => this.aluno = aluno)

        this.$http
            .get('http://localhost:3000/professores')
            .then(res => res.json())
            .then(professores => this.professores = professores)
    },
    methods: {
        editar(){
            this.visualizando = !this.visualizando;
        },
        salvar(_aluno){
            let _alunoEditado = {
                id: _aluno.id,
                nome: _aluno.nome,
                sobrenome: _aluno.sobrenome,
                dataNasc: _aluno.dataNasc,
                professor: _aluno.professor
            }

            this.$http.put(`http://localhost:3000/alunos/${_alunoEditado.id}`, _alunoEditado)
            .then(res => res.json())
            .then(alunos => this.alunos = alunos)

            this.visualizando = !this.visualizando;

        },
        cancelar(){
            this.visualizando = !this.visualizando;
        }

    }
}
</script>

<style scoped>
.colPequeno {
  width: 20%;  
  text-align: right;
  background-color: rgb(100, 253, 202);
  font-weight: bold;
}

input, select {
    margin: 0px;
    padding: 5 5 10 10;
    font-size: 0.9em;
    border: 1px solid silver;
    border-radius: 5px;
    font-family: 'Comic Sans MS', cursive, sans-serif;
    width: 95%;
    background-color: aliceblue;
}

select {
    height: 30px;
    width: 96%;
}

table tr td {
  text-align: left;
}

.btnEditar{
    float: right;
    background-color:rgb(114, 114, 252) ;
    width: 90px;
}

.btnSalvar {
    float: right;
    width: 20%;
    background-color: lightgreen;
}

.btnCancelar{
    float: left;
    width: 20%;
    background-color: rgb(252, 103, 86);
}
</style>
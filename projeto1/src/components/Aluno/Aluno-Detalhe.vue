<template>
    <div v-if="!loading">
        <Titulo :texto="`Aluno: ${aluno.nome} `" :boolVoltar="!visualizando">
            <button v-show="visualizando" class="btn btnEditar" @click="editar()">Editar</button>
        </Titulo>

        <table>
            <tbody>
                <tr>
                    <td class="colPequeno" style="height: 50px;">Foto:</td>
                    <td>
                        <div style="display: flex; align-items: center;">
                            <img v-if="aluno.foto" :src="this.fotoTemp" alt="Foto do aluno" style="width: 150px; height: 150px; object-fit: cover;">
                            <input ref="input" type="file" class="none" accept="image/*" @change="handleFile($event)">
                            <div v-if="!visualizando" style="margin-left: 10px;">
                                <Button v-if="aluno.foto" type="button" class="btn btnFoto" @click="openFileDialog()">Editar Foto</Button>
                                <Button v-else type="button" class="btn btnFoto" @click="openFileDialog()">Inserir Foto</Button>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Matrícula:</td>
                    <td>
                        <label>{{ aluno.id }}</label>
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">CPF:</td>
                    <td>
                        <label>{{ aluno.cpf }}</label>
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
                        <select v-else v-model="aluno.professor.id">
                            <option v-for="(professor, index) in professores" :key="index" v-bind:value="professor.id">
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
                <button class="btn btnSalvar" @click="salvar()">Salvar</button>
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
            visualizando: true,
            loading: true,
            fotoTemp : null
        }
    },
    created() {
        this.carregarProfessor();
    },
    methods: {
        openFileDialog() {
            this.$refs.input.value = null;
            this.$refs.input.click();
        },
        handleFile(evento) {
            const file = evento.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    // Converte o resultado (ArrayBuffer) para byte[]
                    const arrayBuffer = e.target.result;
                    const byteArray = new Uint8Array(arrayBuffer);
                    this.aluno.foto = Array.from(byteArray); // Converte para array de bytes
                    console.log("Imagem convertida para byte[]:", this.aluno.foto);
                };
                reader.readAsArrayBuffer(file); // Lê o arquivo como ArrayBuffer
                this.fotoTemp = URL.createObjectURL(file);
            }
        },
        editar() {
            this.visualizando = !this.visualizando;
        },
        salvar() {
            let _alunoEditado = {
                id: this.aluno.id,
                nome: this.aluno.nome,
                sobrenome: this.aluno.sobrenome,
                dataNasc: this.aluno.dataNasc,
                professorid: this.aluno.professor.id,
                foto: this.aluno.foto
            }

            this.$http.put(`http://localhost:5000/api/aluno/${_alunoEditado.id}`, _alunoEditado)
                .then(res => res.json())
                .then(aluno => this.aluno = aluno)

            this.visualizando = !this.visualizando;

        },
        cancelar() {
            this.visualizando = !this.visualizando;
            this.carregarAluno();
        },
        carregarProfessor() {
            this.$http
                .get('http://localhost:5000/api/professor')
                .then(res => res.json())
                .then(professores => {
                    this.professores = professores;
                    this.carregarAluno();
                });
        },
        carregarAluno() {
            this.$http.get(`http://localhost:5000/api/aluno/${this.idAluno}`)
            .then(res => res.json())
            .then(aluno => {
                this.fotoTemp = `http://localhost:5000/api/aluno/${aluno.id}/foto`
                this.aluno = aluno;
                this.loading = false; 
            });
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

input,
select {
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

.btnEditar {
    float: right;
    background-color: rgb(114, 114, 252);
    width: 90px;
}

.btnSalvar {
    float: right;
    width: 20%;
    background-color: lightgreen;
}

.btnCancelar {
    float: left;
    width: 20%;
    background-color: rgb(252, 103, 86);
}
</style>
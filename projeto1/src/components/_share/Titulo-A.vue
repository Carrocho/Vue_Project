<template>
    <div>
        <div style="display: flex; width:100%">
            <h1>{{ texto }}</h1>
            <div class="voltar">
                <button v-show="boolVoltar" class="btn btnVoltar" @click="voltar()"> Voltar</button>
                <slot></slot>
            </div>
        </div>

    </div>
</template>

<script>
export default {
    props: {
        texto: String,
        boolVoltar: Boolean
    },
    methods:{
        voltar(){
            this.$router.back();
        }
    },
}
</script>

<style scoped>
.voltar {
    width: auto;
    position: relative;
    margin-top: 35px;
    margin-left: auto;
    margin-right: 5px;
}

.btnVoltar {
    background-color: darkgrey;
    width: 90px;
}
</style>
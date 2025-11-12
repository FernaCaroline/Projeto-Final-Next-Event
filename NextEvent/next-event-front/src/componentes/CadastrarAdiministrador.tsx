// Não vejo necessidade de no front termos a listagem de adiministradores, ja que não cabe ao usuário final ver uma lista do banco de dados.
import { useState } from "react";
import Adiministrador from "../interfaces/Adiministrador";
import { error } from "console";
import axios from "axios";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

function CadastrarAdiministrador(){
     return(
        <div>
            <h1>Cadastrar Adiministrador</h1>
        </div>
     )
}

export default CadastrarAdiministrador;
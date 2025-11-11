// Não vejo necessidade de no front termos a listagem de participantes, ja que não cabe ao usuário final ver uma lista do banco de dados.
import { useState } from "react";
import Participante from "../interfaces/Participante";
import { error } from "console";
import axios from "axios";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

function CadastrarParticipante(){
     return(
        <div>
            <h1>Cadastrar Participante</h1>
        </div>
     )
}

export default CadastrarParticipante;
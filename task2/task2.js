const fs = require('fs');
const  { SHA3 } = require("sha3")

const rstream = f => new Promise( (resolve, reject) => {
    const readStream = fs.createReadStream(f);

    const data = []
    readStream.on('data', (chunk) => {
        data.push(chunk)
    })

    readStream.on('end', () => {
        resolve(data)
    })

    readStream.on('error', (err) => {
        reject(err)
    })
})

const fhash = async entry => {
    let data = await rstream(entry)
    const hash = new SHA3(256);
    hash.update(Buffer.concat(data))
    console.log(entry, hash.digest('hex'))
}

for(let entry of fs.readdirSync(".")) fhash(entry)

// резервввв
//
// const fs = require('fs')
// const path = require('path')
//
// const  { SHA3 } = require("sha3")
//
// const ignore = ["node_modules"]
//
// const rstream = f => new Promise( (resolve, reject) => {
//     const readStream = fs.createReadStream(f);
//
//     const data = []
//     readStream.on('data', (chunk) => {
//         data.push(chunk)
//     })
//
//     readStream.on('end', () => {
//         resolve(data)
//     })
//
//     readStream.on('error', (err) => {
//         reject(err)
//     })
// })
//
// const getHash = (entry, data) => {
//     const hash = new SHA3(256);
//     hash.update(Buffer.concat(data))
//     // hash.encode('utf-8')
//     console.log(entry, hash.digest('hex'))
// }
//
// const fhash = async entry =>{
//     let statEntry = fs.statSync(entry)
//     if(statEntry.isFile()){
//         try{
//             let data = await rstream(entry)
//             getHash(entry, data)
//         }
//         catch(e){
//             console.log(e)
//         }
//
//     }
//     else if (statEntry.isDirectory()){
//         console.log(entry)
//         for(let e of fs.readdirSync(entry)) {
//             if(!ignore.includes(e))
//                 fhash(path.join(entry, "\\", e))
//         }
//     }
// }
//
// try{
//     for(let entry of fs.readdirSync(".")) if(!ignore.includes(entry)) fhash(entry)
// }
// catch(e){
//     console.error(e.message)
// }
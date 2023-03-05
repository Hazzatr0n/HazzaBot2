using System;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using NSec.Cryptography;

namespace Bot;

public class Security
{
    // Fields only accessible by the constructor
    private PublicKey PublicKey;
    private SignatureAlgorithm algorithm = SignatureAlgorithm.Ed25519;

    public Security(string pKey)
    {
        // Generate a public key obj from the key provided
        var publicKeyBytes = Convert.FromHexString(pKey);
        this.PublicKey = PublicKey.Import(algorithm, publicKeyBytes, KeyBlobFormat.RawPublicKey);
    }

    public bool CheckValidity(string body, string sig, string timestamp)
    {
        // Checks the validity of the body passed
        var awaiterPayload = Task.Run(() => Encoding.UTF8.GetBytes(timestamp + body));
        var awaiterSig = Task.Run(()=> Convert.FromHexString(sig));

        Task.WaitAll(awaiterPayload, awaiterSig);
        
        var result = algorithm.Verify(this.PublicKey,awaiterPayload.Result, awaiterSig.Result);
        LambdaLogger.Log($"The security was found to be {result}");
        return result;
    }
}